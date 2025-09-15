using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using praktica1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace praktica1;

public partial class korzina : Window
{
    public korzina()
    {
        InitializeComponent();
    }

    public korzina(List<Product> listProducts)
    {
        InitializeComponent();
        TovarBox.ItemsSource = listProducts.ToList();
    }

    private void Order_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        using var context = new ShchetinkinContext();

        int sumcost = context.Products.Select(s => s.ProductCost).Sum();
        

        var order = new Order()
        {
            OrderName = Guid.NewGuid().ToString(),
            OrderDateStart = DateOnly.FromDateTime(DateTime.Now),
            OrderDateEnd = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            OrderStatus = "—борка",
            SumCost = sumcost ,
            
        };
    }

  
}