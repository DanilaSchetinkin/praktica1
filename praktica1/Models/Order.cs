using System;
using System.Collections.Generic;

namespace praktica1.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderName { get; set; } = null!;

    public DateOnly? OrderDateStart { get; set; }

    public DateOnly? OrderDateEnd { get; set; }

    public string? OrderStatus { get; set; }

    public int? SumCost { get; set; }

    public int? UserId { get; set; }
}
