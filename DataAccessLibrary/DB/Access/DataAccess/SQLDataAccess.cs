using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _configuration;

        public string ConnectionStringName { get; } = "Standard";

        public SQLDataAccess(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<List<T>> LoadDataNoParam<T>(string sql, CancellationToken token)
        {
            string connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {

                CommandDefinition command = new(sql, cancellationToken: token);

                var data = await connection.QueryAsync<T>(command:command);

                return data.ToList();
            }
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, CancellationToken token)
        {
            string connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                CommandDefinition command = new(sql, parameters:parameters, cancellationToken:token);

                var data = await connection.QueryAsync<T>(command);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters, CancellationToken token)
        {
            string connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                CommandDefinition command = new(sql, parameters: parameters, cancellationToken: token);

                await connection.ExecuteAsync(command);
            }
        }
    }
}