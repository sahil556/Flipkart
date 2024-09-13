namespace FlipkartTest;
using Flipkart.MVVM.Models;

public class UnitTest1
{
    [Fact]
    public void showProductTest()
    {
        // Arrange Product object for product model
        Product product = new Product
        {
            id = 1,
            title = "Product 1",
            price = 100,
            image = "image1.jpg",
            description = "Product 1 description",
            category = "Category 1",
            rating = new Rating
            {
                count = 10,
                rate = 4.5m
            }
        };
        
    }
}