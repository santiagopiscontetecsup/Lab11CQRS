using Lab11SantiagoPisconte.Application.Services.Interfaces;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;

namespace Lab11SantiagoPisconte.Application.Services.Jobs;

public class TicketCleanupService : ITicketCleanupService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketCleanupService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CleanUpClosedTicketsAsync()
    {
        var closedTickets = await _unitOfWork.Tickets.FindWithIncludeAsync(
            t => t.Status.Trim().ToLower() == "cerrado", 
            t => t.Responses 
        );

        foreach (var ticket in closedTickets)
        {
            if (ticket.Responses != null && ticket.Responses.Any())
            {
                foreach (var response in ticket.Responses)
                {
                    _unitOfWork.Responses.Delete(response);
                }
            }

            _unitOfWork.Tickets.Delete(ticket);
        }

        await _unitOfWork.SaveAsync();

        Console.WriteLine($"{closedTickets.Count()} tickets cerrados y sus respuestas eliminados.");
    }
}