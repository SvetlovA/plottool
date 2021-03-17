using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlotTool.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetData();
    }
}
