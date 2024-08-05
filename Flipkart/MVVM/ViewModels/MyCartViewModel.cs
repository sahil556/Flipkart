using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Flipkart.MVVM.Models;

namespace Flipkart;

public partial class MyCartViewModel: ObservableObject
{
    HttpClient client;
    JsonSerializerOptions options;

    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    [ObservableProperty]
    public ObservableCollection<Product> cartProducts = new ObservableCollection<Product>();


    public MyCartViewModel()
    {
        client = new HttpClient();
        options = new JsonSerializerOptions { WriteIndented = true };
        LoadProducts();
    }

    private async void LoadProducts()
    {
        await Task.Delay(2000);
        string baseUrl = "https://fakestoreapi.com/products";
        try{
            var response = await client.GetAsync(baseUrl);
            if(response.IsSuccessStatusCode)
            {
               using (var responseStream = await response.Content.ReadAsStreamAsync())
               {
                 var data = await JsonSerializer.DeserializeAsync<List<Product>>(responseStream, options);
                 foreach (var product in data)
                 {
                    Products.Add(product);
                 }
               }

               baseUrl = "https://fakestoreapi.com/carts/5";
               var cartResponse = await client.GetAsync(baseUrl);
               if(cartResponse.IsSuccessStatusCode)
               {
                    using (var responseStream = await cartResponse.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<Cart>(responseStream, options);
                        foreach (var product in data.products)
                        {
                            var prod = Products.FirstOrDefault(p => p.id == product.productId);
                            prod.description = product.quantity.ToString();
                            CartProducts.Add(prod);
                        }
                    }
               }
            }
            
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading products: {ex.Message}");
        }
    }

}
