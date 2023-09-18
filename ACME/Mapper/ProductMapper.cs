using ACME.DTO;
using ACME.Models;
using AutoMapper;

namespace ACME.Mapper;

public class ProductMapper: Profile
{
    public ProductMapper()
    {
        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}
