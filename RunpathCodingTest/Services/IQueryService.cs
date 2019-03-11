using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunpathWebApi.Services
{
    public interface IQueryService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string query = null);
    }
}
