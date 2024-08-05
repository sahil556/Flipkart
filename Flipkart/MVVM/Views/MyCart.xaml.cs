namespace Flipkart.MVVM.Views;

public partial class MyCart : ContentPage
{
	public MyCart(MyCartViewModel viewModel) 
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}