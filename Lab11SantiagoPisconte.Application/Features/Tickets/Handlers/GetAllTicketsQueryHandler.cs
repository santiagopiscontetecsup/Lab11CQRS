using Lab11SantiagoPisconte.Application.DTOs.Tickets;
using Lab11SantiagoPisconte.Application.Features.Tickets.Queries;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Tickets.Handlers;

internal class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTicketsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();
        return tickets.Select(t => new TicketDto(t)).ToList();
    }
}