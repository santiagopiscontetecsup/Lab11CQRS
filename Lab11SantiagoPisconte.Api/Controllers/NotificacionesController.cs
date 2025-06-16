using Hangfire;
using Lab11SantiagoPisconte.Application.Services.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Lab11SantiagoPisconte.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificacionesController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificacionesController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("fire-forget")]
    public IActionResult FireForget()
    {
        BackgroundJob.Enqueue(() => _notificationService.SendNotification("usuario1"));
        return Ok("Job encolado (fire-and-forget).");
    }

    [HttpPost("delayed")]
    public IActionResult Delayed()
    {
        BackgroundJob.Schedule(() => _notificationService.SendNotification("usuario2"), TimeSpan.FromMinutes(1));
        return Ok("Job programado (delayed).");
    }
}