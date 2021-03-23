using PlotTool.Repositories.Implementation;
using PlotTool.Services.Implementation;

namespace PlotTool
{
    public static class PlotManagerFactory
    {
        public static IPlotManager CreateFilePlotManager(string[] plotFilesDirectoryPaths) =>
            new PlotManager(new PlotViewService(new FilePlotViewRepository(plotFilesDirectoryPaths)));
    }
}
