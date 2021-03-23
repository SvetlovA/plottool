using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlotTool.Entities;
using PlotTool.Helpers;

namespace PlotTool.Repositories.Implementation
{
    internal class FilePlotViewRepository : IRepository<PlotView>
    {
        private readonly string[] _plotDirectories;

        public FilePlotViewRepository(string[] plotFilesDirectoryPaths)
        {
            _plotDirectories = plotFilesDirectoryPaths ?? throw new ArgumentNullException(nameof(plotFilesDirectoryPaths));
        }

        public Task<IEnumerable<PlotView>> GetData() => FileParser.ParseAsync(_plotDirectories);
    }
}
