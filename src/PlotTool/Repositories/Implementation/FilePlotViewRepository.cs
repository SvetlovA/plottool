using System.Collections.Generic;
using System.Threading.Tasks;
using PlotTool.Entities;

namespace PlotTool.Repositories.Implementation
{
    internal class FilePlotViewRepository : IRepository<PlotView>
    {
        public Task<IEnumerable<PlotView>> GetData() => FileParser.ParseAsync();
    }
}
