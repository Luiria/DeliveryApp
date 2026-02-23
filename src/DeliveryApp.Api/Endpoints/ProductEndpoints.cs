using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Interfaces;

namespace ServiceRequestApp.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("api/products", async (IProductService service) =>
        {
            var all = await service.GetAllAsync();
            return Results.Ok(all);
        });

        app.MapGet("api/products/{id:int}", async (int id, IProductService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item == null ? Results.NotFound() : Results.Ok(item);
        });

        app.MapPost("api/products", async (CreateProductDto dto, IProductService service) =>
        {
            var (ok, error, created) = await service.CreateAsync(dto);
            if (!ok) return Results.BadRequest(new { error });
            return Results.Created($"/api/products/{created!.Id}", created);
        });

        app.MapPut("api/products/{id:int}", async (int id, UpdateProductDto dto, IProductService service) =>
        {
            var (ok, error, updated) = await service.UpdateAsync(id, dto);

            if (!ok)
            {
                if (error == "Not found")
                    return Results.NotFound();
                return Results.BadRequest(new { error });
            }

            return Results.Ok(updated);
        });

    }
}
