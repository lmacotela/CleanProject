using AppointmentSearch.Api.Extension;
using AppointmentSearch.Application;
using AppointmentSearch.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppointmentSearch.Api", Version = "v1" });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
    app.MapOpenApi();
}

app.ApplyMigration();
app.SeedData();
app.UseCustomExceptionHandler();
app.MapControllers();

app.Run();
