using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Application.Validation;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Service.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return products.Select(ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null ? null : ToDto(product);
    }

    public async Task<(bool ok, string error, ProductDto? created)>
        CreateAsync(CreateProductDto dto)
    {
        var (ok, error) = ProductValidators.Validate(dto);
        if (!ok) return (false, error, null);

        var entitie = new Product
        {
            Name = dto.Name.Trim(),
            Price = dto.Price,
        };

        var product = await _repo.AddAsync(entitie);
        return (true, "", ToDto(product));
    }

    public async Task<(bool ok, string error, ProductDto? updated)>
        UpdateAsync(int id, UpdateProductDto dto)
    {
        var (ok, error) = ProductValidators.Validate(dto);
        if (!ok) return (false, error, null);

        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return (false, "Not found", null);

        existing.Name = dto.Name.Trim();
        existing.Price = dto.Price;

        var updated = await _repo.UpdateAsync(existing);
        return updated == null
            ? (false, "Update failed", null)
            : (true, "", ToDto(updated));
    }

    // ── Private helper ──────────────────────────────
    private static ProductDto ToDto(Product p)
        => new ProductDto(
            p.Id,
            p.Name,
            p.Price
        );
}