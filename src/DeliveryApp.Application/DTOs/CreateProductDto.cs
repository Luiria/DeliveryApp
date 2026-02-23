namespace DeliveryApp.Application.DTOs;

public record CreateProductDto(
    string Name,
    decimal Price
);