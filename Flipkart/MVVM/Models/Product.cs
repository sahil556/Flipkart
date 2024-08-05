using CommunityToolkit.Mvvm.ComponentModel;

namespace Flipkart.MVVM.Models;

public partial class Product: ObservableObject
{
    public int id { get; set; }
    public string title { get; set; }
    public decimal price { get; set; }
    public string image { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public Rating rating { get; set; } 
   
}

public class Rating
{
    public int count { get; set; }
    public decimal rate { get; set; }
}

public class ProductDTO
{
    public int id { get; set; }
    public string title { get; set; }
    public decimal price { get; set; }
    public string image { get; set; }
    public string category { get; set; }
    public int quantity { get; set; }
}