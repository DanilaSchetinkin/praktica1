using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using praktica1.Models;
using System;
using System.IO;
using System.Linq;

namespace praktica1;

public partial class AdminWindow : Window
{
    private string selectedImagePath;

    public AdminWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });

        if (files.Count > 0)
        {
            selectedImagePath = files[0].Path.LocalPath;
            MainImage.Source = new Bitmap(selectedImagePath);
        }
    }

    private void Product_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
        string productName = ProductName.Text;
        string productCaption = ProductCaption.Text;
        int productCost = Convert.ToInt32(ProductCost.Text);

        
        string imageFileName = null;
        if (!string.IsNullOrEmpty(selectedImagePath))
        {
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            string extension = Path.GetExtension(selectedImagePath);
            imageFileName = $"img_{DateTime.Now.Ticks}{extension}";
            string destPath = Path.Combine(imagesFolder, imageFileName);

            File.Copy(selectedImagePath, destPath);
            imageFileName = "Images/" + imageFileName;
        }

        
        var newProduct = new Product()
        {
            ProductName = productName,
            ProductCost = productCost,
            ProductCaption = productCaption,
            ProductImage = imageFileName
        };

        
        using var context = new ShchetinkinContext();

        
        newProduct.ProductId = context.Products.Max(p => p.ProductId) + 1;

        context.Products.Add(newProduct);
        context.SaveChanges();

        
        ProductName.Text = "";
        ProductCost.Text = "";
        ProductCaption.Text = "";
        MainImage.Source = null;
        selectedImagePath = null;
    }
}