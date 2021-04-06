using System.Runtime.CompilerServices;
using PlotTool.Entities;
using PlotTool.Repositories.Implementation;
using PlotTool.Services.Implementation;

[assembly:InternalsVisibleTo("PlotTool.Tests")]

namespace PlotTool
{
    public static class PlotManagerFactory
    {
        public static IPlotManager CreateFilePlotManager(params string[] plotFilesDirectoryPaths) =>
            new PlotManager(new PlotViewService(new FilePlotViewRepository(new InputPlotData
            {
                PlotFilesDirectoryPaths = plotFilesDirectoryPaths
            })));

        public static IPlotManager CreateFilePlotManager(string plotName, string[] plotFilesDirectoryPaths) =>
            new PlotManager(new PlotViewService(new FilePlotViewRepository(new InputPlotData
            {
                PlotName = plotName,
                PlotFilesDirectoryPaths = plotFilesDirectoryPaths
            })));
    }
}
