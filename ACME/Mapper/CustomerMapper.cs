using ACME.DTO;
using ACME.Models;
using AutoMapper;

namespace ACME.Mapper;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<CustomerRequest, Customer>();
        CreateMap<Customer, Customer>();
        CreateMap<Customer, CustomerResponse>();
    }
}
