using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlotTool.Repositories
{
    internal interface IRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetData();
    }
}
