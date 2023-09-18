using ACME.DTO;
using ACME.Models;

namespace ACME.Repository
{
    public interface IOrderRepository
    {
        int Add(OrderRequest orderRequest);
        void AddOrderItem(int orderId, OrderItemRequest orderItemRequest);
        void AddOrderPayment(int orderId, OrderPaymentRequest orderPaymentRequest);
        void DeleteOrderItem(int orderId, OrderItemRequest orderItemRequest);
        void DeleteOrderPayment(int orderId, OrderPaymentRequest orderPaymentRequest);
        Order? Get(int orderId);
        Order GetOrderValidateCanUpdate(int orderId);
        void ShipOrderAync(int orderId);
        void MapOrderItemProduct(OrderItem orderItem);
    }
}