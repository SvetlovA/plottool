using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PlotTool.Entities;

namespace PlotTool.Helpers
{
    internal static class FileParser
    {
        private const char CoordinatesSeparator = ' ';
        private const string TraceNamePattern = @"^(([^\d\s]+.*)|(.+[^\d\s]+))";

        public static Task<IEnumerable<PlotView>> ParseAsync(InputPlotData plotData)
        {
            if (plotData == null)
            {
                throw new ArgumentNullException(nameof(plotData));
            }

            if (plotData.PlotPaths == null)
            {
                throw new ArgumentNullException(nameof(plotData.PlotPaths));
            }

            return GetPlotViews(plotData);
        }

        private static async Task<IEnumerable<PlotView>> GetPlotViews(InputPlotData plotData)
        {
            var result = new List<PlotView>(plotData.PlotPaths.Length);

            foreach (var plotPath in plotData.PlotPaths)
            {
                if (!Directory.Exists(plotPath) && !File.Exists(plotPath))
                {
                    throw new Exception($"Directory or file {plotPath} not exists");
                }

                result.Add(new PlotView
                {
                    PlotName = plotData.PlotName ?? plotPath,
                    Traces = (await GetTraceViewsAsync(plotPath)).ToArray()
                });
            }

            return result;
        }

        private static async Task<IEnumerable<TraceView>> GetTraceViewsAsync(string plotPath)
        {
            var filePaths = File.GetAttributes(plotPath) == FileAttributes.Directory
                ? Directory.GetFiles(plotPath, "*", new EnumerationOptions
                {
                    RecurseSubdirectories = true
                }) : new []{ plotPath };

            var result = new List<TraceView>(filePaths.Length);
            foreach (var filePath in filePaths)
            {
                IEnumerable<string> lines = await File.ReadAllLinesAsync(filePath);
                if (TryGetTraceNameByFileHeader(lines.FirstOrDefault(), filePath, out var traceName))
                {
                    lines = lines.Skip(1);
                }

                var coordinates = GetCoordinates(lines).ToArray();

                result.Add(new TraceView
                {
                    TraceName = traceName,
                    X = coordinates.Select(x => x.Item1).ToArray(),
                    Y = coordinates.Select(x => x.Item2).ToArray()
                });
            }

            return result;
        }

        private static IEnumerable<(double, double)> GetCoordinates(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var coordinates = line.Split(CoordinatesSeparator, StringSplitOptions.RemoveEmptyEntries);

                if (coordinates.Length != 2)
                {
                    throw new Exception("Coordinates count must be 2");
                }

                yield return (double.Parse(coordinates[0]), double.Parse(coordinates[1]));
            }
        }

        private static bool TryGetTraceNameByFileHeader(string line, string filePath, out string traceName)
        {
            var result = line != null && Regex.IsMatch(line, TraceNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            traceName = result ? line : GetTraceNameByFilePath(filePath);

            return result;
        }


        private static string GetTraceNameByFilePath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            return !string.IsNullOrEmpty(fileInfo.Extension)
                ? fileInfo.Name.Replace(fileInfo.Extension, string.Empty)
                : fileInfo.Name;
        }
    }
}
