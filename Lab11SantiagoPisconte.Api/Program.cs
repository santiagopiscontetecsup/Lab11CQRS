using Lab11SantiagoPisconte.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios e infraestructura
builder.Services.AddApplicationServices(builder.Configuration);

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
app.MapControllers();

app.Run();