using Lab11SantiagoPisconte.Application.DTOs.Tickets;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Tickets.Queries;

public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>>;