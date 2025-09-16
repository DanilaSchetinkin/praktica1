using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
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
        public int ProductId { get; set; }
        public Bitmap? ImagePath { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public string ProductCaption { get; set; }

        public string ColorCost { get; set; }
    }


    private int _currentUserId;
    private string _currentUserLogin;

   

    private List<Tovars> _tovars;

    private List<Product> _products = new List<Product>();

    public Products()
    {
        InitializeComponent();
    }

    public Products(int userId, string userLogin)
    {
        InitializeComponent();
        LoadData();

        _currentUserId = userId;
        _currentUserLogin = userLogin;

        userLoginBox.Text = _currentUserLogin;
        userIdBox.Text = _currentUserId.ToString();

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
                ImagePath = s.ImagePath,
                ProductName = s.ProductName,
                ProductCost = s.ProductCost,
                ProductCaption = s.ProductCaption,
                ColorCost = s.ColorCost,
                ProductId = s.ProductId
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

    public void Button_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int idProduct = (int)(sender as Button).Tag;
        int idUser = (int)(sender as Button).Tag;

        using (ShchetinkinContext context = new ShchetinkinContext())
        {
            var product = context.Products.FirstOrDefault(x=>x.ProductId == idProduct);
            _products.Add(product!);
            var user = context.Users.FirstOrDefault(x=>x.UserId == idUser);
            

        }

    }

    private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        korzina korzina = new korzina(_products, _currentUserId, _currentUserLogin);
        korzina.Show();
        this.Close();
    }

}