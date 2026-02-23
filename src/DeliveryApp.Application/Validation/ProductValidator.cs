using DeliveryApp.Application.DTOs;

namespace DeliveryApp.Application.Validation;

public class ProductValidators
{
    public static (bool ok, string error) Validate(CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 50)
            return (false, "A name is required and must be <= 50 chars.");

        if (dto.Price <= 0)
            return (false, "A price is required");

        return (true, "");
    }

    public static (bool ok, string error) Validate(UpdateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 50)
            return (false, "A name is required and must be <= 50 chars.");

        if (dto.Price <= 0)
            return (false, "A price is required");

        return (true, "");
    }

}