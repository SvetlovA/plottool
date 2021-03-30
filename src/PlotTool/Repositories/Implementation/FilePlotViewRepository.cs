using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlotTool.Entities;
using PlotTool.Helpers;

namespace PlotTool.Repositories.Implementation
{
    internal class FilePlotViewRepository : IRepository<PlotView>
    {
        private readonly InputPlotData _plotData;

        public FilePlotViewRepository(InputPlotData plotData)
        {
            _plotData = plotData ?? throw new ArgumentNullException(nameof(plotData));
        }

        public Task<IEnumerable<PlotView>> GetData() => FileParser.ParseAsync(_plotData);
    }
}
