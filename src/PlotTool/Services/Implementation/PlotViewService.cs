using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlotTool.Entities;
using PlotTool.Helpers;
using PlotTool.Repositories;

namespace PlotTool.Services.Implementation
{
    public class PlotViewService : IPlotViewService
    {
        private readonly IRepository<PlotView> _plotViewRepository;

        public PlotViewService(IRepository<PlotView> plotViewRepository)
        {
            _plotViewRepository = plotViewRepository ?? throw new ArgumentNullException(nameof(plotViewRepository));
        }

        public Task<IEnumerable<PlotView>> GetAllPlots() => _plotViewRepository.GetData();

        public async Task<IEnumerable<PlotView>> GetAggregatedPlots()
        {
            var plotViews = await GetAllPlots();
            return plotViews
                .Select(plotView => new PlotView
                {
                    PlotName = plotView.PlotName,
                    Traces = new List<TraceView>
                    {
                        new()
                        {
                            TraceName = $"Aggregated {plotView.PlotName}",
                            X = plotView.Traces.Select(z => z.X).OrderBy(z => z.Count).Last(),
                            Y = plotView.Traces.Select(z => z.Y).Aggregate(CollectionsHelper.MergeCollections)
                        }
                    }
                });
        }
    }
}
