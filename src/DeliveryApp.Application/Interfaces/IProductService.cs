using DeliveryApp.Application.DTOs;

namespace DeliveryApp.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int id);

    Task<(bool ok, string error, ProductDto? updated)>
        UpdateAsync(int id, UpdateProductDto dto);

    Task<(bool ok, string error, ProductDto? created)>
        CreateAsync(CreateProductDto dto);


}