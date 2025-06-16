using Hangfire;
using Hangfire.MySql;
using Lab11SantiagoPisconte.Application.Services.Jobs;
using Lab11SantiagoPisconte.Application.Services.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11SantiagoPisconte.Infrastructure.Configuration;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UseStorage(new MySqlStorage(
                configuration.GetConnectionString("DefaultConnection"),
                new MySqlStorageOptions()
            )));

        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder UseHangfireDashboardAndJobs(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire");

        RecurringJob.AddOrUpdate<NotificationService>(
            "job-notificacion-diaria",
            service => service.SendNotification("usuario_diario"),
            Cron.Daily);
        
        RecurringJob.AddOrUpdate<TicketCleanupService>(
            "job-cleanup-closed-tickets",
            service => service.CleanUpClosedTicketsAsync(),
            Cron.Daily); 

        return app;
    }
}