using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Tickets.Commands;

public record UpdateTicketStatusCommand : IRequest<bool>
{
    public Guid TicketId { get; set; }
    public string Status { get; set; } = string.Empty;

    public UpdateTicketStatusCommand(Guid ticketId, string status)
    {
        TicketId = ticketId;
        Status = status;
    }
}