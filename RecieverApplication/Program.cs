using MassTransit;
using Microsoft.OpenApi.Models;
using RecieverApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Receiver Application", Version = "v1" });
});

// Configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        // Register consumers globally (if needed)

        // Configure RabbitMQ host
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });

        // Define a receiving endpoint named "SendTutorial"
        cfg.ReceiveEndpoint("SendTutorial", e =>
        {
            // Register the SenderTutorial consumer for the endpoint
            e.Consumer<SenderConsumer>(context);
        });
        cfg.ReceiveEndpoint("PublishTutorial", e =>
        {
            // Register the PublisherTutorial consumer for the endpoint
            e.Consumer<PublisherConsumer>(context);
        });  
        cfg.ReceiveEndpoint("ReqResTutorial", e =>
        {
            // Register the RequestResponseTutorial consumer for the endpoint
            e.Consumer<RequestResponseConsumer>(context);
        });
    });
    x.AddConsumer<SenderConsumer>();
    x.AddConsumer<PublisherConsumer>();
    x.AddConsumer<RequestResponseConsumer>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Receiver Application");
});

app.Run();
