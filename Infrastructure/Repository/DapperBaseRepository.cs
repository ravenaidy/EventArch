using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Connection.Contracts;

namespace Infrastructure.Repository
{
    public class DapperBaseRepository : IDisposable
    {
        private bool _disposed;
        private readonly IConnectionFactory _connectionFactory;
        private IDbConnection _connection;

        protected DapperBaseRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        protected IDbConnection Connection
        {
            get
            {
                if (_connection == null && !_disposed)
                    _connection = _connectionFactory.CreateOpenConnection();
                return _connection;
            }
        }
        protected async Task<int> ExecuteAsync(string sql, object parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            var cmd = new CommandDefinition(sql, parameters, commandType: commandType);
            return await Connection.ExecuteAsync(cmd);
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, object parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            var cmd = new CommandDefinition(sql, parameters, commandType: commandType);
            return await Connection.ExecuteScalarAsync<T>(cmd);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var cmd = new CommandDefinition(sql, parameters, commandType: commandType);
            return await Connection.QueryAsync<T>(cmd);
        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var cmd = new CommandDefinition(sql, parameters, commandType: commandType);
            return await Connection.QueryFirstOrDefaultAsync<T>(cmd);
        }

        public void Dispose()
        {
            _disposed = true;

            try
            {
                _connection?.Dispose();
            }
            catch { }

            _connection = null;
        }
    }
}