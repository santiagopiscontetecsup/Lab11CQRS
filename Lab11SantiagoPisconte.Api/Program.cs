using Hangfire;
using Lab11SantiagoPisconte.Api.Configuration;
using Lab11SantiagoPisconte.Application.Services.Notifications;
using Lab11SantiagoPisconte.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios e infraestructura
builder.Services.AddApplicationServices(builder.Configuration); 
builder.Services.AddHangfireServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();

app.UseHangfireDashboardAndJobs();
app.MapControllers();

app.Run();