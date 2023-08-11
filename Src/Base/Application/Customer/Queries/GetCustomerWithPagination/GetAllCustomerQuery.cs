using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetAllCustomerQuery : IRequest<IEnumerable<CustomerInfo>>
{
}

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<CustomerInfo>>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetAllCustomerQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerInfo>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetAllCustomers();

        return entity;
    }
}

