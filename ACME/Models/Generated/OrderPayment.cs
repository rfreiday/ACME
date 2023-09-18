using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ACME.Models;

[Table("OrderPayment")]
public partial class OrderPayment
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PaymentType { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderPayments")]
    public virtual Order Order { get; set; } = null!;
}
