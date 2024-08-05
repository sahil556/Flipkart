using Flipkart.MVVM.Models;

namespace Flipkart.MVVM.Models;

public class Cart
{
    public int id { get; set; }
    public int userId { get; set; }
    public DateTime date { get; set; }

    public List<CartItem> products { get; set; }
}

public class CartItem{
    public int productId { get; set; }
    public int quantity { get; set; }
}
