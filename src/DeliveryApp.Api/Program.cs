using DeliveryApp.Application.Interfaces;
using DeliveryApp.Infrastructure.Repositories;
using DeliveryApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection"),
   sql => sql.EnableRetryOnFailure())
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.MapGet("/", () => "ok!");

app.MapProductEndpoints();

app.Run();
