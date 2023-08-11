Implementing CleanArchitecture with Dapper and EF core and  authentication and authorization in  Web Api by using Identity and JWT and RedisDistributedCaching in   Web Api  and Logging with elasticsearch in  Web Api    and ReportBusinessObjectStimulSoft in Web and HangFire in Web Mvc

## Dapper
Dapper is an micro ORM  for the Microsoft .NET platform: it provides a framework for mapping an object-oriented domain model to a traditional relational database.

## Steps
### 1.Install-Packages ON nugget
 |PackageName|
 |---|
|1.Dapper|
|2.System.Data.SqlClient|

### 2.Create DapperContext class

  ```

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
  ```

### 3.add configuration in Program or startup

```
  var DefaultConnection = configuration.GetConnectionString("DefaultConnection");
   services.AddSingleton<DapperDBContext>(provider => new DapperDBContext(DefaultConnection));
  ```
      
### 4.Using Deppar methods (ExecuteScalar, Query, …)

#### ExecuteScalar

Executes query that return a single value.

  ```

using (var db = _context.CreateConnection())
        {
            return await db.ExecuteScalarAsync<int>(SqlCommandConst.sqlGetCountCustomersNew);
        }
  ```

#### Querying Single Row

Executes a query that return a row(first or single)

•	QuerySingle(result= a row)
•	QuerySingleOrDefault(result= a row or null)
•	QueryFirst(result= first row)
•	QueryFirstOrDefault(result= first row or null)

   ```
 using (var db = _context.CreateConnection())
        {
            return await db.QueryFirstOrDefaultAsync<CustomerInfo>(SqlCommandConst.sqlGetFirstCustomers);
        }
  ```

#### Query

    Executes a query, returning and maps it to a list of dynamic objects  

```
       using (var db = _context.CreateConnection())
        {
            return await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetAllCustomers);
        }
   ```


#### QueryMultiple

   Execute a multi query that returns multiple result sets, and access each in turn.

•	Step 1: execute multiple queries using the QueryMultiple
•	Step 2: get the returned results  with Read, ReadFirst, ReadFirstOrDefault, ReadSingle ,ReadSingleOrDefault 


 ```
 using (var db = _context.CreateConnection())
        {
            using (var result = await db.QueryMultipleAsync(SqlCommandConst.sqlGetFirstAndLastCountCustomers))
            {
                var _first = await result.ReadFirstAsync<CustomerInfo>();
                var _last = await result.ReadFirstAsync<CustomerInfo>();
                return new Tuple<CustomerInfo, CustomerInfo>(_first, _last);
            }

        }
 ```

#### Execute
Execute a query and  return the number of affected rows.
It is used to execute INSERT, UPDATE, and DELETE statement.

 ```
 using (var db = _context.CreateConnection())
        {
            return await db.ExecuteAsync(SqlCommandConst.sqlUpdateEmailNullCustomers);
        }
```

#### ExecuteReader
Execute a query and return an IDataReader .

```
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
```

#### parameterized query
use Parameter in queries

```
 using (var db = _context.CreateConnection())
  {
    var _result = await db.QueryAsync<CustomerInfo>(SqlCommandConst.sqlGetCsutomerByEmail, new { Email = Email });

     return _result;
   }
```

## Config Elasticsearch or Serilog or JWT or Identity or Clean Architecture ReportBusinessObjectStimulSoft in Web and HangFire in Web Mvc
https://github.com/abolfazlSadeqi/ReportBusinessObjectStimulSoft/blob/master/README.md




## Tech Specification:

1.authentication and authorization in  Web Api by using Identity and JWT

2.Dapper
  
3.RedisDistributedCaching in  Web Api
  
4.Logging with Serilog and elasticsearch in  Web Api   
  
5.ReportBusinessObjectStimulSoft  in  Web Mvc
  
6.Read And Write Config  in  Web Mvc
  
7.TDD(XUnit) include 1.UnitTest 2.IntegrationTests
  
8.BDD (SpecFlow)
  
9.EFCore7 
  
10.Net7
  
11.Swagger UI



