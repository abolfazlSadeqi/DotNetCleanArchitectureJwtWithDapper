using Infrastructure_Dapper.Entity;
using Infrastructure_Dapper.Persistence;

using System.Data;

using Dapper;
using Hangfire;
using Application.Common.Interfaces;
using Application.Common.Models;
using Common.Other;
using System.Collections.Generic;

namespace Infrastructure_Dapper.Services.Custom.CustomerCommand;

public class CustomerService : ICustomerService
{
    private readonly DapperDBContext _context;


    public CustomerService(DapperDBContext context) => _context = context;

    /// <summary>
    /// Executes query that return a single value.
    /// </summary>
    /// <returns></returns>
    public async Task<int?> GetCountCustomersNew()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.ExecuteScalarAsync<int>(SqlCommandConst.sqlGetCountCustomersNew);
        }

    }

    /// <summary>
    /// Executes a query that return a row(first or single)
    /// •	QuerySingle(result= a row)
    /// •	QuerySingleOrDefault(result= a row or null)
    /// •	QueryFirst(result= first row)
    /// •	QueryFirstOrDefault(result= first row or null)
    /// </summary>
    /// <returns></returns>
    public async Task<CustomerInfo?> GetFirstCustomers()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.QueryFirstOrDefaultAsync<CustomerInfo>(SqlCommandConst.sqlGetFirstCustomers);
        }

    }

    /// <summary>
    /// Executes a query, returning and maps it to a list of dynamic objects 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CustomerInfo>> GetAllCustomers()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetAllCustomers);
        }

    }

    /// <summary>
    /// Execute a multi query that returns multiple result sets, and access each in turn.•	Step 1: execute multiple queries using the QueryMultiple•	Step 2: get the returned results  with Read, ReadFirst, ReadFirstOrDefault, ReadSingle, ReadSingleOrDefault
    /// </summary>
    /// <returns></returns>
    public async Task<Tuple<CustomerInfo, CustomerInfo>> GetFirstAndLastCustomers()
    {
        using (var db = _context.CreateConnection())
        {
            using (var result = await db.QueryMultipleAsync(SqlCommandConst.sqlGetFirstAndLastCountCustomers))
            {
                var _first = await result.ReadFirstAsync<CustomerInfo>();
                var _last = await result.ReadFirstAsync<CustomerInfo>();
                return new Tuple<CustomerInfo, CustomerInfo>(_first, _last);
            }

        }
    }
    /// <summary>
    /// Execute a query and return an IDataReader .
    /// </summary>
    /// <returns></returns>
    public async Task<Tuple<long?, DateTime?>> GetValidCsutomer()
    {
        using (var db = _context.CreateConnection())
        {
            long count = 0;
            DateTime maxCreateDate = default;


            var _result = await db.ExecuteReaderAsync(SqlCommandConst.sqlGetValidCsutomer);
            while (_result.Read())
            {
                int _f = _result.GetInt32(0);
                count = _f;
                maxCreateDate = _result.GetDateTime(1);

            }

            return new Tuple<long?, DateTime?>(count, maxCreateDate);
        }
    }
    /// <summary>
    /// use Parameter in queries
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CustomerInfo>> GetCsutomerByEmail(string Email)
    {
        using (var db = _context.CreateConnection())
        {
            var _result = await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetCsutomerByEmail, new { Email = Email });

            return _result;
        }
    }

    /// <summary>
    /// Execute a query and  return the number of affected rows. It is used to execute INSERT, UPDATE, and DELETE statement
    /// </summary>
    /// <returns></returns>
    public async Task<int?> UpdateEmailNullCustomers()
    {
        using (var db = _context.CreateConnection())
        {
            return await db.ExecuteAsync(SqlCommandConst.sqlUpdateEmailNullCustomers);
        }
    }

}
