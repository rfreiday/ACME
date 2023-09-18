using ACME.DTO;
using ACME.Models;
using AutoMapper;

namespace ACME.Mapper;

public class OrderMapper: Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderItem, OrderItemResponse>();
        CreateMap<OrderPayment, OrderPaymentResponse>();

        CreateMap<OrderRequest, Order>();
        CreateMap<OrderItemRequest, OrderItem>();
        CreateMap<OrderPaymentRequest, OrderPayment>();
    }
}
