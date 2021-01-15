using Dapper;
using DataAccessLibrary.DB;
using Microsoft.EntityFrameworkCore;
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
        private readonly AuthContext _context;

        public SQLDataAccess(AuthContext context)
        {
            this._context = context;
        }

        public async Task<List<T>> LoadDataNoParam<T>(string sql, CancellationToken token)
        {
            using (IDbConnection connection = _context.Database.GetDbConnection())
            {

                CommandDefinition command = new(sql, cancellationToken: token);

                var data = await connection.QueryAsync<T>(command:command);

                return data.ToList();
            }
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, CancellationToken token)
        {
            using (IDbConnection connection = _context.Database.GetDbConnection())
            {
                CommandDefinition command = new(sql, parameters:parameters, cancellationToken:token);

                var data = await connection.QueryAsync<T>(command);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters, CancellationToken token)
        {
            using (IDbConnection connection = _context.Database.GetDbConnection())
            {
                CommandDefinition command = new(sql, parameters: parameters, cancellationToken: token);

                await connection.ExecuteAsync(command);
            }
        }
    }
}