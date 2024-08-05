namespace Flipkart;

using Flipkart.MVVM.Views;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Routing.RegisterRoute("LoginPage", typeof(LoginPage));
		Routing.RegisterRoute("ProductPage", typeof(ProductPage));
		MainPage = new AppShell();
	}
}

