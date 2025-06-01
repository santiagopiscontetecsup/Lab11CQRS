using Lab11SantiagoPisconte.Application.DTOs.Tickets;
using Lab11SantiagoPisconte.Application.Features.Tickets.Queries;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Tickets.Handlers;

internal class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(request.TicketId);
        if (ticket == null) return null;
        return new TicketDto(ticket);
    }
}