namespace Lab11SantiagoPisconte.Application.Services.Interfaces;

public interface ITicketCleanupService
{
    Task CleanUpClosedTicketsAsync();
}