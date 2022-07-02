using MassTransit;
using Stocks.API.Consumer;
using Stocks.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<StockInMemory>();

var rabbitMq = builder.Configuration.GetValue<string>("RabbitMQ");

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PaymentFailedEventConsumer>();
    configure.AddConsumer<OrderCreatedEventConsumer>();
    configure.UsingRabbitMq((context, configurator) =>
    {
       
        configurator.Host(rabbitMq, "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

      

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
