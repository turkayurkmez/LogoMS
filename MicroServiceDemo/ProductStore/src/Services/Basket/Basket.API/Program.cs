using Basket.API;
using Basket.API.Consumers;
using Basket.API.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddHttpClient<ICatalogService, CatalogService>();
builder.Services.AddSingleton<BasketRepository>();

var rabbitMq = builder.Configuration.GetValue<string>("RabbitMQ");
Console.WriteLine($"..............DİKKAT RabbitMQ bağlantısı:{rabbitMq}");

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PriceChangedConsumer>();
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(rabbitMq, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
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
