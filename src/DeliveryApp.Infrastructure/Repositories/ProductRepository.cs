using Microsoft.EntityFrameworkCore;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync()
        => await _db.Products.OrderByDescending(p => p.Id).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id)
    => await _db.Products.FindAsync(id);

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existing = await _db.Products.FindAsync(product.Id);

        if (existing == null) return null;

        existing.Name = product.Name;
        existing.Price = product.Price;

        await _db.SaveChangesAsync();
        return existing;
    }
    
    public async Task<Product> AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

}