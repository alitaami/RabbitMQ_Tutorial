using Common;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sender Application", Version = "v1" });
});

// Configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });
    });
    // register request client
    x.AddRequestClient<BalanceUpdate>();
});

// This will start the bus and register consumers
builder.Services.AddMassTransitHostedService(); 

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sender Application");
});

app.Run();
