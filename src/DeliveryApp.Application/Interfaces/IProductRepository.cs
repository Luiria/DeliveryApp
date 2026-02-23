
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> UpdateAsync(Product product);
    Task<Product> AddAsync(Product product);
}