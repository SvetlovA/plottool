﻿using System;
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
        private const string TracePattern = @"^(([^\d\s]+.*)|(.+[^\d\s]+))";

        public static Task<IEnumerable<PlotView>> ParseAsync(InputPlotData plotData)
        {
            if (plotData == null)
            {
                throw new ArgumentNullException(nameof(plotData));
            }

            if (plotData.PlotFilesDirectoryPaths == null)
            {
                throw new ArgumentNullException(nameof(plotData.PlotFilesDirectoryPaths));
            }

            return GetPlotViews(plotData);
        }

        private static async Task<IEnumerable<PlotView>> GetPlotViews(InputPlotData plotData)
        {
            var result = new List<PlotView>(plotData.PlotFilesDirectoryPaths.Length);

            foreach (var directoryPath in plotData.PlotFilesDirectoryPaths)
            {
                if (!Directory.Exists(directoryPath))
                {
                    throw new Exception($"Directory {directoryPath} not exists");
                }

                result.Add(new PlotView
                {
                    PlotName = plotData.PlotName ?? directoryPath,
                    Traces = (await GetTraceViewsAsync(directoryPath)).ToArray()
                });
            }

            return result;
        }

        private static async Task<IEnumerable<TraceView>> GetTraceViewsAsync(string directoryPath)
        {
            var filePaths = Directory.GetFiles(directoryPath);
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
            var result = line != null && Regex.IsMatch(line, TracePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            traceName = result ? line : GetTraceNameByFilePath(filePath);

            return result;
        }


        private static string GetTraceNameByFilePath(string filePath) =>
            filePath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
    }
}
