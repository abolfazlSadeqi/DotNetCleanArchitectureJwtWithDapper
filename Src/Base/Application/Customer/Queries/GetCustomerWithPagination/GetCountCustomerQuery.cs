using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetCountCustomerQuery : IRequest<int?>
{
}

public class GetCountCustomerQueryHandler : IRequestHandler<GetCountCustomerQuery, int?>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetCountCustomerQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> Handle(GetCountCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetCountCustomersNew();

        return entity;
    }
}

