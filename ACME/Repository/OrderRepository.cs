using ACME.Data;
using ACME.DTO;
using ACME.Exceptions;
using ACME.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACME.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly AcmeContext _context;

        public OrderRepository(AcmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Order? Get(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.OrderPayments)
                .FirstOrDefault(c => c.Id == orderId);
            return order;
        }

        public Order GetOrderValidateCanUpdate(int orderId)
        {
            var order = Get(orderId);
            if (order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            if (order.Status == OrderStatus.Shipped)
            {
                throw new OrderShippedError(orderId);
            }
            return order;
        }

        public int Add(OrderRequest orderRequest)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == orderRequest.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException($"Invalid Customer id [{orderRequest.CustomerId}].");
            }
            var order = _mapper.Map<Order>(orderRequest);
            foreach (var item in order.OrderItems)
            {
                MapOrderItemProduct(item);
            }
            order.Recalculate();
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public void AddOrderItem(int orderId, OrderItemRequest orderItemRequest)
        {
            // Get / validate the order
            var order = GetOrderValidateCanUpdate(orderId);

            // Scenario 1: Existing item
            var existingItem = order.OrderItems.FirstOrDefault(i => i.ProductId == orderItemRequest.ProductId);
            if (existingItem is not null)
            {
                existingItem.Quantity += orderItemRequest.Quantity;
                existingItem.Total = existingItem.Quantity * existingItem.Price;
            }

            // Scenario 2: New item
            else
            {
                var item = _mapper.Map<OrderItem>(orderItemRequest);
                MapOrderItemProduct(item);
                order.OrderItems.Add(item);
            }

            // Recalcualte / save
            order.Recalculate();
            _context.SaveChanges();
        }

        public void DeleteOrderItem(int orderId, OrderItemRequest orderItemRequest)
        {
            // Get / validate the order
            var order = GetOrderValidateCanUpdate(orderId);

            // 
            var existingItem = order.OrderItems.FirstOrDefault(i => i.ProductId == orderItemRequest.ProductId);
            if (existingItem is null)
            {
                throw new NotFoundException($"Order [{orderId}] does not contain any items with product id [{orderItemRequest.ProductId}].");
            }

            // If the request quantity is LESS THAN the existing quantity, simply reduce the quantity
            if (orderItemRequest.Quantity < existingItem.Quantity)
            {
                existingItem.Quantity -= orderItemRequest.Quantity;
                existingItem.Total = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                order.OrderItems.Remove(existingItem);
            }

            // Recalcualte / save
            order.Recalculate();
            _context.SaveChanges();
        }

        public void AddOrderPayment(int orderId, OrderPaymentRequest orderPaymentRequest)
        {
            // Get / validate the order
            var order = GetOrderValidateCanUpdate(orderId);

            // Validate that the amount paid does not exceed the amount due
            if (orderPaymentRequest.Amount > order.AmountDue)
            {
                throw new OrderPaymentExceedsAmountDueException(orderPaymentRequest);
            }

            // Add payment
            var payment = _mapper.Map<OrderPayment>(orderPaymentRequest);
            order.OrderPayments.Add(payment);

            // Save
            _context.SaveChanges();
        }

        public void DeleteOrderPayment(int orderId, OrderPaymentRequest orderPaymentRequest)
        {
            // Get / validate the order
            var order = GetOrderValidateCanUpdate(orderId);

            // Find a matching payment
            var existingPayment = order.OrderPayments.FirstOrDefault(i =>
                i.PaymentType == orderPaymentRequest.PaymentType &&
                i.Amount == orderPaymentRequest.Amount);

            if (existingPayment is null)
            {
                throw new NotFoundException($"Order [{orderId}] does not contain a matching payment.");
            }

            order.OrderPayments.Remove(existingPayment);

            // Recalcualte / save
            order.Recalculate();
            _context.SaveChanges();
        }

        public void ShipOrderAync(int orderId)
        {
            var order = GetOrderValidateCanUpdate(orderId);
            if (order.OrderItems.Count == 0)
            {
                throw new OrderMissingItemsException(order);
            }
            if (order.Overpaid)
            {
                throw new OrderCustomerOverpaidException(order);
            }
            if (!order.PaidInFull)
            {
                throw new OrderNotPaidException(order);
            }
            order.Shipped = true;
            _context.SaveChanges();
        }

        public void MapOrderItemProduct(OrderItem orderItem)
        {
            // Get the product and update the item
            var product = _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
            if (product is null)
            {
                throw new NotFoundException($"Product id [{orderItem.ProductId}] is invalid.");
            }
            orderItem.ProductDescription = product.Description;
            orderItem.Price = product.Price;
            orderItem.Total = orderItem.Quantity * orderItem.Price;
        }
    }
}
