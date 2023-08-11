using System;
using System.Data;
using System.Data.SqlClient;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure_Dapper.Persistence;

public class DapperDBContext: IDapperDBContext
{
 
    private readonly string _connectionString;
    public DapperDBContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

}
