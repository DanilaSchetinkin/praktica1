using System;
using System.Collections.Generic;

namespace praktica1.Models;

public partial class OrdersService
{
    public int OrdersServiceId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
