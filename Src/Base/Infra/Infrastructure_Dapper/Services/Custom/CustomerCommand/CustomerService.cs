using Infrastructure_Dapper.Entity;
using Infrastructure_Dapper.Persistence;

using System.Data;

using Dapper;
using Hangfire;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Infrastructure_Dapper.Services.Custom.CustomerCommand;

public class CustomerService : ICustomerService
{
    private readonly DapperDBContext _context;


    public CustomerService(DapperDBContext context) => _context = context;

    public async Task<int?> GetCountCustomersNew()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.ExecuteScalarAsync<int>(SqlCommandConst.sqlGetCountCustomersNew);
        }

    }


    public async Task<CustomerInfo?> GetFirstCustomers()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.QueryFirstOrDefaultAsync<CustomerInfo>(SqlCommandConst.sqlGetFirstCustomers);
        }

    }


    public async Task<IEnumerable<CustomerInfo>> GetAllCustomers()
    {

        using (var db = _context.CreateConnection())
        {
            return await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetAllCustomers);
        }

    }


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
    public async Task<IEnumerable<CustomerInfo>> GetCsutomerByEmail(string Email)
    {
        using (var db = _context.CreateConnection())
        {
            var _result = await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetCsutomerByEmail, new { Email = Email });

            return _result;
        }
    }

    public async Task<int?> UpdateEmailNullCustomers()
    {
        using (var db = _context.CreateConnection())
        {
            return await db.ExecuteAsync(SqlCommandConst.sqlUpdateEmailNullCustomers);
        }
    }

}
