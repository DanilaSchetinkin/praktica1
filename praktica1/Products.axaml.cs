using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using praktica1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace praktica1;

public partial class Products : Window
{

    public class Tovars
    {
        public Bitmap? ImagePath { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public string ProductCaption { get; set; }

        public string ColorCost { get; set; }
    }

    private List<Tovars> _tovars;

    public Products()
    {
        InitializeComponent();
        LoadData();

        SortComboBoxByCost.SelectionChanged += SortComboBox_Cost;
        SortComboBoxByName.SelectionChanged += SortComboBox_Name;
        SearchBox.TextChanged += SearchBoxChanging;
    }


    private void LoadData()
    {
        using var context = new ShchetinkinContext();
        _tovars = context.Products
            .Select(s => new Tovars
            {
                ImagePath = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/" + s.ProductImage),
                ProductName = s.ProductName,
                ProductCost = s.ProductCost,
                ProductCaption = s.ProductCaption,
                ColorCost = s.ColorCost
            })
            .ToList();

        TovarBox.ItemsSource = _tovars;

    }

    private void SortComboBox_Cost(object sender, EventArgs e)
    {

        var sorted = _tovars.ToList();

        switch (SortComboBoxByCost.SelectedIndex)
        {
            case 0:
                sorted = _tovars.ToList();
                break;
            case 1:
                sorted = _tovars.OrderBy(s => s.ProductCost).ToList();
                break;
            case 2:
                sorted = _tovars.OrderByDescending(s => s.ProductCost).ToList();
                break;
        }

        TovarBox.ItemsSource = sorted;

    }

    private void SortComboBox_Name(object sender, EventArgs e)
    {
        var sorted = _tovars.ToList();

        switch (SortComboBoxByName.SelectedIndex)
        {
            case 0:
                sorted = _tovars.ToList();
                break;
                case 1:
                sorted = _tovars.OrderBy(s => s.ProductName).ToList();
                break;
                case 2:
                sorted = _tovars.OrderByDescending(s => s.ProductName).ToList();
                break;
        }
        TovarBox.ItemsSource = sorted;
    }

    private void SearchBoxChanging(object sender, EventArgs e)
    {
        var searchText = SearchBox.Text?.ToLower() ?? string.Empty;

        if (string.IsNullOrEmpty(searchText))
        {
            TovarBox.ItemsSource = _tovars;
        }
        else
        {
            var searchItem = _tovars
                .Where(x =>
                (x.ProductName?.ToLower().Contains(searchText) ?? false)
                )
                .ToList();
            TovarBox.ItemsSource = searchItem;
        }
    }

    private void Button_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _tovars

    }

    
}