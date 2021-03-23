using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PlotTool.Entities;

namespace PlotTool.Helpers
{
    internal static class FileParser
    {
        public static async Task<IEnumerable<PlotView>> ParseAsync(string[] plotDirectories)
        {
            if (plotDirectories == null)
            {
                throw new ArgumentNullException(nameof(plotDirectories));
            }

            var result = new List<PlotView>();

            foreach (var directoriesPath in plotDirectories)
            {
                var plotView = new PlotView
                {
                    PlotName = directoriesPath,
                    Traces = new List<TraceView>()
                };

                if (!Directory.Exists(directoriesPath))
                {
                    throw new Exception($"Directory {directoriesPath} not exists");
                }

                foreach (var filePath in Directory.GetFiles(directoriesPath, "*.txt"))
                {
                    var lines = await File.ReadAllLinesAsync(filePath);
                    var traceView = new TraceView
                    {
                        TraceName = filePath.Split(new[] {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar},
                            StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
                        X = new List<double>(),
                        Y = new List<double>()
                    };

                    foreach (var line in lines)
                    {
                        var coordinates = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if (coordinates.Length != 2)
                        {
                            throw new Exception("Coordinates count must be 2");
                        }

                        traceView.X.Add(double.Parse(coordinates[0]));
                        traceView.Y.Add(double.Parse(coordinates[1]));
                    }
                    plotView.Traces.Add(traceView);
                }
                result.Add(plotView);
            }

            return result;
        }
    }
}
