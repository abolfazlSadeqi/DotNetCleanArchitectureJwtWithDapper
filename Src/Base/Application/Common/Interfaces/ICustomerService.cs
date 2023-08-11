
using System.Data;
using Application.Common.Models;
using Hangfire;

namespace Application.Common.Interfaces;

public interface ICustomerService
{


     Task<int?> GetCountCustomersNew();



    Task<CustomerInfo?> GetFirstCustomers();

   

    Task<IEnumerable<CustomerInfo>> GetAllCustomers();


    Task<Tuple<CustomerInfo, CustomerInfo>> GetFirstAndLastCustomers();

    Task<Tuple<long?, DateTime?>> GetValidCsutomer();
    Task<IEnumerable<CustomerInfo>> GetCsutomerByEmail(string Email);



    Task<int?> UpdateEmailNullCustomers();

}
