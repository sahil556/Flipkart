using Flipkart.MVVM.Models;

namespace Flipkart.Services;

public class ProductService: RestService
{
    public async Task<IEnumerable<Product>> GetProductsAsync(string sortOrder = "asc")
    {
        return await GetAsync<IEnumerable<Product>>($"products?sort={sortOrder}");
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await GetAsync<Product>($"products/{productId}");
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string category, string sortOrder = "asc")
    {
        return await GetAsync<IEnumerable<Product>>($"products/category/{category}?sort={sortOrder}");
    }
}
