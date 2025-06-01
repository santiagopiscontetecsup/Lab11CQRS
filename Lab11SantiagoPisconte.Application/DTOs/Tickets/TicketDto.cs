using Lab11SantiagoPisconte.Api.Models;

namespace Lab11SantiagoPisconte.Application.DTOs.Tickets;

public class TicketDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }

    public TicketDto(Ticket ticket)
    {
        Id = ticket.TicketId;
        Title = ticket.Title;
        Status = ticket.Status;
    }
}