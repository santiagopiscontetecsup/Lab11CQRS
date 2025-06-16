using Hangfire;
using Lab11SantiagoPisconte.Application.Services.Interfaces;
using Lab11SantiagoPisconte.Application.Services.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11SantiagoPisconte.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador,Soporte")]
public class JobsController : ControllerBase
{
    private readonly ITicketCleanupService _cleanupService;

    public JobsController(ITicketCleanupService cleanupService)
    {
        _cleanupService = cleanupService;
    }

    [HttpPost("cleanup-closed-tickets")]
    public IActionResult TriggerCleanup()
    {
        BackgroundJob.Enqueue(() => _cleanupService.CleanUpClosedTicketsAsync());
        return Ok("Job de limpieza de tickets cerrados encolado.");
    }

    [HttpPost("cleanup-closed-tickets-now")]
    public async Task<IActionResult> CleanupNow()
    {
        await _cleanupService.CleanUpClosedTicketsAsync();
        return Ok("Limpieza de tickets cerrados ejecutada inmediatamente.");
    }
}