using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.MVVM.Models;
using Flipkart.Services;

namespace Flipkart;

public partial class MyCartViewModel: ObservableObject
{
    HttpClient client;
    JsonSerializerOptions options;

    private readonly CartService cartService;
    private readonly ProductService productService;
    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    [ObservableProperty]
    public ObservableCollection<Product> cartProducts = new ObservableCollection<Product>();

    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public bool isUserLoggedIn;

    public MyCartViewModel(CartService _cartService, ProductService _productService)
    {
        client = new HttpClient();
        cartService = _cartService;
        productService = _productService;
        options = new JsonSerializerOptions { WriteIndented = true };
        LoadProducts();
    }

    [RelayCommand]
    public void Init()
    {
        
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
