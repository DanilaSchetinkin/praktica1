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

    int userId;

    List<Product> products;

    public korzina()
    {
        InitializeComponent();
    }

    public korzina(List<Product> listProducts, int userIdBox, string userLoginBox)
    {
        InitializeComponent();
        TovarBox.ItemsSource = listProducts.ToList();
        userId = userIdBox;
        products = listProducts;
        
    }

    private void Order_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        using var context = new ShchetinkinContext();



        var order = new Order()
        {
            OrderId = context.Orders.OrderBy(X=>X.OrderId).LastOrDefault().OrderId + 1,
            OrderName = Guid.NewGuid().ToString(),
            OrderDateStart = DateOnly.FromDateTime(DateTime.Now),
            OrderDateEnd = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            OrderStatus = "—борка",
            SumCost = 0,
            UserId = userId

        };

        context.Orders.Add(order);  
        context.SaveChanges();

        foreach (var item in products) 
        {
            var orderService = new OrdersService()
            {
                OrdersServiceId = context.OrdersServices.OrderBy(X => X.OrdersServiceId).LastOrDefault().OrdersServiceId + 1,
                OrderId = context.Orders.OrderBy(X => X.OrderId).LastOrDefault().OrderId + 1,
                ProductId = item.ProductId,
            };
            context.OrdersServices.Add(orderService);
            context.SaveChanges();

        }


       

        int sumcost = context.Products.Select(s => s.ProductCost).Sum();

        var orderRedactionSum = context.Orders.OrderBy(X => X.OrderId).LastOrDefault();

        orderRedactionSum.SumCost = sumcost;
        context.Orders.Update(orderRedactionSum);
        context.SaveChanges();
    }

  
}