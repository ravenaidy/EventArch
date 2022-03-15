using System.Data;

namespace Infrastructure.Connection.Contracts
{
    public interface IConnectionFactory
    {
        IDbConnection CreateOpenConnection();
    }
}