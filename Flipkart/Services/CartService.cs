using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Flipkart.MVVM.Models;
using Flipkart.Services;

namespace Flipkart.Services;

public class CartService: RestService
{
    private Cart myCart {get; set;}
    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        if(myCart == null)
            myCart = (await GetAsync<List<Cart>>($"carts/user/{userId}")).FirstOrDefault();
        
        return myCart;
    }

    public async Task<Cart> AddProductAsync(int productId, int quantity)
    {
        string userId = await SecureStorage.Default.GetAsync("userid");
        if(string.IsNullOrEmpty(userId))
        {
            CancellationToken token = new CancellationToken();
            var toast = Toast.Make("You need to Login First", ToastDuration.Short, 14);
            toast.Show(token);
            return null;
        }
        var myCart = (await GetCartByUserIdAsync(userId));
        if(myCart.products.FirstOrDefault(p => p.productId == productId) == null)
        {   
            myCart.products.Add( new CartItem { productId = productId, quantity = quantity});
        }
        else
        {
            var product = myCart.products.FirstOrDefault(p => p.productId == productId);
            product.quantity += quantity;
        }   
        return await PutAsync<Cart>($"carts/{myCart.id}", myCart);
    }

    public async Task<Cart> DecrementProductQuantity(int cartId, int productId, int quantity)
    {
        if(myCart.products.FirstOrDefault(p => p.productId == productId) != null)
        {
            var product = myCart.products.FirstOrDefault(p => p.productId == productId);
            product.quantity -= quantity;
            if(product.quantity <= 0)
                myCart.products.Remove(product);

            return await PutAsync<Cart>($"carts/{myCart.id}", myCart);
        }
        return null;
    }
}
