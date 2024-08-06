using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.MVVM.Models;
using Flipkart.Services;

namespace Flipkart.MVVM.ViewModels;

public partial class MyCartViewModel: ObservableObject
{
    HttpClient client;
    JsonSerializerOptions options;

    private readonly CartService cartService;
    private readonly ProductService productService;
    
    [ObservableProperty]
    public ObservableCollection<Product> products = new ObservableCollection<Product>();

    [ObservableProperty]
    public ObservableCollection<Product> cartProducts = new ObservableCollection<Product>();

    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public bool isUserLoggedIn;

    public int cartId {get; set;}

    public MyCartViewModel(CartService _cartService, ProductService _productService)
    {
        client = new HttpClient();
        cartService = _cartService;
        productService = _productService;
        options = new JsonSerializerOptions { WriteIndented = true };
    }

    [RelayCommand]
    public void Init()
    {
        LoadProducts();
    }

    [RelayCommand]
    public async void DecrementProductQuantity(int productId)
    {   
        IsBusy = true;
        var response = await cartService.DecrementProductQuantity(cartId, (productId), 1);
        if(response != null)
        {
            LoadProducts();
            CancellationToken cancellationToken = new CancellationToken();
            var toast = Toast.Make("Product Quantity Decremented",ToastDuration.Short, 14);
            await toast.Show(cancellationToken);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async void IncrementProductQuantity(int productId)
    {   
        IsBusy = true;
        var response = await cartService.AddProductAsync((productId), 1);
        if(response != null)
        {
            LoadProducts();
            CancellationToken cancellationToken = new CancellationToken();
            var toast = Toast.Make("Product Quantity Incremented",ToastDuration.Short, 14);
            await toast.Show(cancellationToken);
        }
        IsBusy = false;
    }
    private async void LoadProducts()
    {
        IsBusy = true;
        try
        {
            string userId = await SecureStorage.Default.GetAsync("userid");
            if(string.IsNullOrEmpty(userId))
            {
                IsUserLoggedIn = false;
                return;
            }
            IsUserLoggedIn = true;
            var response = await productService.GetProductsAsync();
            if(response != null)
            {
                foreach (var product in response)
                {
                Products.Add(product);
                }

               var cartResponse = await cartService.GetCartByUserIdAsync(userId);
               if(cartResponse != null)
               {
                    cartId = cartResponse.id;
                    CartProducts = new ObservableCollection<Product>();
                    foreach (var product in cartResponse.products)
                    {
                        var prod = Products.FirstOrDefault(p => p.id == product.productId);
                        prod.description = product.quantity.ToString();
                        CartProducts.Add(prod);
                    }
               }
            }
            
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading products: {ex.Message}");
        }
        finally{
            IsBusy = false;
        }
    }

}
