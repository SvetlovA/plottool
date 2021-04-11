using System.Runtime.CompilerServices;
using PlotTool.Entities;
using PlotTool.Repositories.Implementation;
using PlotTool.Services.Implementation;

[assembly:InternalsVisibleTo("PlotTool.Tests")]

namespace PlotTool
{
    public static class PlotManagerFactory
    {
        public static IPlotManager CreateFilePlotManager(params string[] plotPaths) =>
            new PlotManager(new PlotViewService(new FilePlotViewRepository(new InputPlotData
            {
                PlotPaths = plotPaths
            })));

        public static IPlotManager CreateFilePlotManager(string plotName, string[] plotPaths) =>
            new PlotManager(new PlotViewService(new FilePlotViewRepository(new InputPlotData
            {
                PlotName = plotName,
                PlotPaths = plotPaths
            })));
    }
}
