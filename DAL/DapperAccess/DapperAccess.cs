using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAL.DapperAccess
{
    public class DapperAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public DapperAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PortfolioConnectionString");
        }

        public T QueryFirst<T>(string storedProcedure, object parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        return db.QueryFirst<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public IEnumerable<T> Query<T>(string storedProcedure, object parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        return db.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public int Execute(string storedProcedure, object parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        return db.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public int BulkExecute(string storedProcedure, List<object> parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                         db.Execute(storedProcedure, parameters, transaction, commandType: CommandType.StoredProcedure, commandTimeout: 120);

                        transaction.Commit();

                        return 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
