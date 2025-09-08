using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using praktica1.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace praktica1;

public partial class Products : Window
{

    public class Tovars
    {
        //public Bitmap? ImagePath { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public string ProductCaption { get; set; }
    }

    private ObservableCollection<Tovars> _tovars;

    public Products()
    {
        InitializeComponent();
        LoadData();
    }


    private void LoadData()
    {
        using var context = new ShchetinkinContext();
        _tovars = new ObservableCollection<Tovars>(context.Products
            .Select(s => new Tovars
            {
                //ImagePath = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/" + s.ProductImage),
                ProductName = s.ProductName,
                ProductCost = s.ProductCost,
                ProductCaption = s.ProductCaption
            })
            .ToList());

        TovarBox.ItemsSource = _tovars;
    }
}