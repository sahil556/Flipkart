using Flipkart.MVVM.ViewModels;

namespace Flipkart.MVVM.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}