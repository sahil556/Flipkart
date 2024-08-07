using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.MVVM.Models;
using Flipkart.Services;

namespace Flipkart.MVVM.ViewModels;

[QueryProperty(nameof(ProductId),"Id")]
public partial class ProductPageViewModel: ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public string productId = "test Id";

    [ObservableProperty]
    public ObservableCollection<Product> similarProducts = new ObservableCollection<Product>();

    [ObservableProperty]
    public Product product;

    private CartService cartService;

    public ProductPageViewModel(CartService _cartService)
    {
        cartService = _cartService;
    }
    INotificationManagerService notificationManager =
    Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();

    [RelayCommand]
    public async void AddProductToCart()
    {
        if(Product != null)
        {
           var response =  await cartService.AddProductAsync(Product.id, 1);
           if(response != null)
           {
                CancellationToken cancellationToken = new CancellationToken();
                var toast = Toast.Make("Product Added", ToastDuration.Short, 14);
                await toast.Show(cancellationToken);
                notificationManager.SendNotification("Product Added", $"{Product.title} has been added to your cart");
           }
        }
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.ContainsKey("product"))
            {
                Product = query["product"] as Product;
                var products = query["similarProducts"] as List<Product>;
                foreach(var product in products)
                {
                    SimilarProducts.Add(product);
                }
            }
        }
}
