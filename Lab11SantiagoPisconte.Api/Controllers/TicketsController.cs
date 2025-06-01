using Lab11SantiagoPisconte.Application.DTOs.Tickets;
using Lab11SantiagoPisconte.Application.Features.Tickets.Commands;
using Lab11SantiagoPisconte.Application.Features.Tickets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11SantiagoPisconte.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador,Soporte")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _mediator.Send(new GetAllTicketsQuery());
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(Guid id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTicketStatus(Guid id, [FromBody] UpdateTicketStatusDto request)
    {
        var updated = await _mediator.Send(new UpdateTicketStatusCommand(id, request.Status));
        if (!updated) return NotFound();
        return NoContent();
    }
}