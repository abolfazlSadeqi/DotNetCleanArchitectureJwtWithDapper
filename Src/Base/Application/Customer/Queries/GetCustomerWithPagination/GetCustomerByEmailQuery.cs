using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetCustomerByEmailQuery : IRequest<IEnumerable<CustomerInfo>>
{
    public string Email { set; get; }
}

public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, IEnumerable<CustomerInfo>>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetCustomerByEmailQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerInfo>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetCsutomerByEmail(request.Email);

        return entity;
    }
}

