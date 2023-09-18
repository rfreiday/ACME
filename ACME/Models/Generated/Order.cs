using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ACME.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "money")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "money")]
    public decimal Taxes { get; set; }

    [Column(TypeName = "money")]
    public decimal Total { get; set; }

    public bool Shipped { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
}
