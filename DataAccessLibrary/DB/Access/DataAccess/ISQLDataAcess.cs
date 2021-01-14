using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ISQLDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, CancellationToken cancellation);
        Task SaveData<T>(string sql, T parameters, CancellationToken cancellation);
        Task<List<T>> LoadDataNoParam<T>(string sql, CancellationToken cancellation);
    }
}