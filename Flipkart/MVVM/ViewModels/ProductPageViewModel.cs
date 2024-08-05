using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.MVVM.Models;

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
