using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace praktica1.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductCost { get; set; }

    public string ColorCost
    {
        get
        {
            string color = "White";
            if(ProductCost > 20000)
            {
                color = "Red";
            }
            if(ProductCost < 20000)
            {
                color = "Green";
            }
            return color;
        }
    }

    public string? ProductImage { get; set; }

    public Bitmap ImagePath => new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/" + ProductImage);

    public string? ProductCaption { get; set; }
}
