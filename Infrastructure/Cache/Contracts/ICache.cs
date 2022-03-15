using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Cache.Contracts
{
    public interface ICache
    {
        Task<T> Get<T>(string cacheKey, CancellationToken cancellationToken );
        Task Set<T>(string cacheKey, T response, CancellationToken cancellationToken, int timeToLive = 10) where T : new();
    }
}