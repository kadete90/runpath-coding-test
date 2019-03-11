using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunpathCodingTest.Services
{
    public interface IQueryService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string query);
    }
}
