using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetFirstCustomerQuery : IRequest<CustomerInfo?>
{
}

public class GetFirstCustomerQueryHandler : IRequestHandler<GetFirstCustomerQuery, CustomerInfo?>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetFirstCustomerQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerInfo?> Handle(GetFirstCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetFirstCustomers();

        return entity;
    }
}

