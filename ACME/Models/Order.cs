namespace ACME.Models
{
    public partial class Order
    {
        public void Recalculate()
        {
            Subtotal = OrderItems.Sum(item => item.Total);
            Taxes = Math.Round(Subtotal * DefaultValue.SalesTax / 100, 2);
            Total = Subtotal + Taxes;
        }

        public decimal TotalPayments => OrderPayments.Sum(payment => payment.Amount);
        public decimal AmountDue => Total - TotalPayments;
        public bool PaidInFull => Total > 0 && TotalPayments >= Total;
        public bool Overpaid => TotalPayments > Total;
        public OrderStatus Status => GetOrderStatus();
        private OrderStatus GetOrderStatus()
        {
            if (Shipped)
                return OrderStatus.Shipped;
            if (Overpaid)
                return OrderStatus.ChangeDue;
            if (PaidInFull)
                return OrderStatus.PaidInFull;
            if (OrderItems.Count == 0)
                return OrderStatus.New;
            return OrderStatus.InProgress;
        }
    }
}
