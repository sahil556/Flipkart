using Flipkart.MVVM.Models;
using Flipkart.Services;

namespace Flipkart.Services;

public class CartService: RestService
{
    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return (await GetAsync<List<Cart>>($"carts/user/{userId}")).FirstOrDefault();
    }
}
