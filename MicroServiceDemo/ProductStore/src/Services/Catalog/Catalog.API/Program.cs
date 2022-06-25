using Catalog.Business;
using Catalog.DataAccess.Data;
using Catalog.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository,EFProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var defaultHost = builder.Configuration.GetValue<string>("DefaultHost");
var connectionString = builder.Configuration.GetConnectionString("db");
connectionString = connectionString.Replace("[HOST]", defaultHost);
builder.Services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("Catalog.DataAccess")));


var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CatalogDbContext>();
PrepareDb.Initialize(dbContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();