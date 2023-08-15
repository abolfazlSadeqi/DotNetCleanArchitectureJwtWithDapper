using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomeresWithPagination;
using Common.HelperDistributedCache;
using Common.UI.Cache;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Models;
using PublicApi.Controllers.Base;
using System;

namespace PublicAPI.Controllers.Public;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AdminLevel2")]
public class CustomersWithIMemoryCacheController : ApiControllerBase
{
    private readonly ILogger<CustomersWithIMemoryCacheController> _logger;
    
    private readonly IMemoryCache memoryCacheService;
    public CustomersWithIMemoryCacheController(ILogger<CustomersWithIMemoryCacheController> logger,  IMemoryCache memoryCacheService)
    {
        _logger = logger;
       // 
      this.memoryCacheService = memoryCacheService;
    }




    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetCustomersWithPagination(
        [FromQuery] GetCustomersWithPaginationQuery query) => await _GetCustomersWithPagination(query);


    #region
    private async Task<ActionResult<PaginatedList<CustomerDto>>> _GetCustomersWithPagination([FromQuery] GetCustomersWithPaginationQuery query)
    {
        _logger.LogInformation("start_Customer");
       // memoryCacheService.TryGetValue(ListCache.CustomerCacheKey, null);


        if (!memoryCacheService.TryGetValue(ListCache.CustomerCacheKey, out IEnumerable<CustomerDto>? CustomerDtos))
        {

            GetCustomersWithPaginationQuery querynew = new GetCustomersWithPaginationQuery();
            querynew.PageNumber = 1;

            querynew.PageSize = int.MaxValue;
            var _listCustomerDtoalls = Mediator.Send(querynew);

             memoryCacheService.Set(ListCache.CustomerCacheKey, _listCustomerDtoalls.Result.Items);

            _logger.LogInformation("Count_Customer =" + _listCustomerDtoalls.Result.Items.Count);

            return await Mediator.Send(query);
        }

        var item = CustomerDtos.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToList();
        PaginatedList<CustomerDto> paginatedList = new PaginatedList<CustomerDto>(
           item, CustomerDtos.Count(), query.PageNumber, query.PageSize);

        _logger.LogInformation("Count_Customer=" + item.Count);


        return new ActionResult<PaginatedList<CustomerDto>>(paginatedList);
    }

    #endregion

    [HttpGet("Get/{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        if (!memoryCacheService.TryGetValue(ListCache.CustomerCacheKey, out IEnumerable<CustomerDto>? PersonDtos))
        {
            return await Mediator.Send(new GetCustomerQuery { Id = id });
        }

        return PersonDtos.FirstOrDefault(d => d.Id == id);



    }

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
    {
        memoryCacheService.Remove(ListCache.CustomerCacheKey);
        return await Mediator.Send(command);
    }



    [HttpPut("Update")]
    public async Task<ActionResult<int>> Update(UpdateCustomerCommand command)
    {
        memoryCacheService.Remove(ListCache.CustomerCacheKey);
        return await Mediator.Send(command);


    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCustomerCommand { Id = id });
        memoryCacheService.Remove(ListCache.CustomerCacheKey);
        return Ok();
    }




}

