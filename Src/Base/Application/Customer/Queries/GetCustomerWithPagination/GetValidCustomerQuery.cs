using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetValidCustomerQuery : IRequest<Tuple<long?, DateTime?>>
{
}

public class GetValidCustomerQueryHandler : IRequestHandler<GetValidCustomerQuery, Tuple<long?, DateTime?>>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public GetValidCustomerQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Tuple<long?, DateTime?>> Handle(GetValidCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetValidCsutomer();

        return entity;
    }
}

