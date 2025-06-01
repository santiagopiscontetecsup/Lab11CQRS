using Lab11SantiagoPisconte.Application.Features.Tickets.Commands;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Tickets.Handlers;

internal class UpdateTicketStatusHandler : IRequestHandler<UpdateTicketStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(request.TicketId);
        if (ticket == null) return false;

        ticket.Status = request.Status;
        await _unitOfWork.SaveAsync();

        return true;
    }
}