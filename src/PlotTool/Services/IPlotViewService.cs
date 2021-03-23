using System.Collections.Generic;
using System.Threading.Tasks;
using PlotTool.Entities;

namespace PlotTool.Services
{
    internal interface IPlotViewService
    {
        public Task<IEnumerable<PlotView>> GetAllPlots();
        public Task<IEnumerable<PlotView>> GetAggregatedPlots();
    }
}
