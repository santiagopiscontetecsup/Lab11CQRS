using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Application.Services.Interfaces;
using Lab11SantiagoPisconte.Application.Services.Jobs;
using Lab11SantiagoPisconte.Application.Services.Notifications;
using Lab11SantiagoPisconte.Domain.Interfaces.Repositories;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using Lab11SantiagoPisconte.Infrastructure.Persistence.Repositories;
using Lab11SantiagoPisconte.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11SantiagoPisconte.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
    { 
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<PisconteTicketSystemContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        
        services.AddTransient<NotificationService>();
        
        services.AddTransient<ITicketCleanupService, TicketCleanupService>();

        
        return services;
    }
}