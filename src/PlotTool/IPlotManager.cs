using System.Threading.Tasks;

namespace PlotTool
{
    public interface IPlotManager
    {
        Task DrawAll();
        Task DrawAllPlotViews();
        Task DrawAggregatedPlot();
    }
}
