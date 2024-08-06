using System.Collections.ObjectModel;
using System.Text.Json;
using Flipkart.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Flipkart.MVVM.ViewModels;

public partial class HomePageViewModel
{
    public List<string> CarouselOptions { get; set; }
	public List<string> SmallItems { get; set; }
	public List<string> MedItems { get; set; }
    public string Category { get; set; }
    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    HttpClient client;
    JsonSerializerOptions options;
    public HomePageViewModel()
    {
        client = new HttpClient();
        options = new JsonSerializerOptions { WriteIndented = true };
        FillCarouselOptions();
        LoadProducts();
    }

    
    private async void FillCarouselOptions()
    {
        CarouselOptions =  new List<string>{
			"cimga",
			"cimgb",
			"cimgc",
			"cimgd",
		};
		MedItems = new List<string>{
			"medimg1.jpg",
			"medimg2.jpg",
			"medimg3.jpg",
            "medimg4.jpg",
			"medimg5.jpg",
			"medimg1.jpg",
			"medimg2.jpg",
			"medimg3.jpg",
		};
		SmallItems = new List<string>{
			"smallimg1.jpg",
			"smallimg2.jpg",
			"smallimg3.jpg"
		};

    }

    private async void LoadProducts()
    {
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
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading products: {ex.Message}");
        }
    }

    [RelayCommand]
    public void ShowProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.id == id);
        var similarProducts = Products.Where(p => p.category == product.category).ToList();

        var navigationParam = new Dictionary<string, Object>{
            {"product", product},
            {"similarProducts", similarProducts}
        };
        Shell.Current.GoToAsync($"ProductPage", navigationParam);
    }

}
