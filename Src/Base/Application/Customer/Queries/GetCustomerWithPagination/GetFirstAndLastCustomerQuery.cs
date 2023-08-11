using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetFirstAndLastCustomerQuery : IRequest<Tuple<CustomerInfo, CustomerInfo>>
{
}

public class GetFirstAndLastCustomerQueryHandler : IRequestHandler<GetFirstAndLastCustomerQuery, Tuple<CustomerInfo, CustomerInfo>>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetFirstAndLastCustomerQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Tuple<CustomerInfo, CustomerInfo>> Handle(GetFirstAndLastCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetFirstAndLastCustomers();

        return entity;
    }
}

