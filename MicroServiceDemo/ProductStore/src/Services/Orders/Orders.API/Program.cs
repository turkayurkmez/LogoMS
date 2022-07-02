using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.API.Consumers;
using Orders.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultHost = builder.Configuration.GetValue<string>("DefaultHost");
var connectionString = builder.Configuration.GetConnectionString("db");
connectionString = connectionString.Replace("[HOST]", defaultHost);
builder.Services.AddDbContext<OrdersDbContext>(opt => opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("Orders.API")));

var rabbitMq = builder.Configuration.GetValue<string>("RabbitMQ");

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PaymentCompletedEventConsumer>();
    configure.AddConsumer<PaymentFailedEventConsumer>();
    configure.AddConsumer<StockNotReservedEventConsumer>();

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

app.Services.CreateScope().ServiceProvider.GetRequiredService<OrdersDbContext>().Database.Migrate();

app.UseAuthorization();

app.MapControllers();

app.Run();
