using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class UpdateEmailNullCustomersQuery : IRequest<int?>
{
}

public class UpdateEmailNullCustomersQueryHandler : IRequestHandler<UpdateEmailNullCustomersQuery, int?>
{
    private readonly ICustomerService _context;
    private readonly IMapper _mapper;

    public UpdateEmailNullCustomersQueryHandler(ICustomerService context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> Handle(UpdateEmailNullCustomersQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.UpdateEmailNullCustomers();

        return entity;
    }
}

