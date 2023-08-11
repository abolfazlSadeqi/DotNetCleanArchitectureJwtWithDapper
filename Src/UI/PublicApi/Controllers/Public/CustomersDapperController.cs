using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomeresWithPagination;
using Common.HelperDistributedCache;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Models;
using PublicApi.Controllers.Base;
using System;

namespace PublicAPI.Controllers.Public;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AdminLevel2")]
public class CustomersDapperController : ApiControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly IDistributedCache _DistributedCache;

    public CustomersDapperController(ILogger<CustomersController> logger, IDistributedCache DistributedCache)
    {
        _logger = logger;
        _DistributedCache = DistributedCache;
    }



    [HttpGet("GetCountCustomerQuery")]
    public async Task<ActionResult<int?>> GetCountCustomerQuery()
    {
        return await Mediator.Send(new GetCountCustomerQuery());
    }


    [HttpGet("GetFirstCustomerQuery")]
    public async Task<ActionResult<CustomerInfo?>> GetFirstCustomerQuery()
    {
        return await Mediator.Send(new GetFirstCustomerQuery());
    }
  

    [HttpGet("GetAllCustomerQuery")]
    public async Task<IEnumerable<CustomerInfo>> GetAllCustomerQuery()
    {
        return await Mediator.Send(new GetAllCustomerQuery());
    }


    [HttpGet("GetFirstAndLastCustomerQuery")]
    public async Task<Tuple<CustomerInfo, CustomerInfo>> GetFirstAndLastCustomerQuery()
    {
        return await Mediator.Send(new GetFirstAndLastCustomerQuery());
    }


    [HttpGet("GetValidCustomerQuery")]
    public async Task<Tuple<long?, DateTime?>> GetValidCustomerQuery()
    {
        return await Mediator.Send(new GetValidCustomerQuery());
    }

    [HttpGet("GetCustomerByEmailQuery")]
    public async Task<IEnumerable<CustomerInfo>> GetCustomerByEmailQuery(string Email)
    {
        return await Mediator.Send(new GetCustomerByEmailQuery() { Email = Email });
    }
    [HttpGet("UpdateEmailNullCustomersQuery")]
    public async Task<int?> UpdateEmailNullCustomersQuery()
    {
        return await Mediator.Send(new UpdateEmailNullCustomersQuery());
    }





}

