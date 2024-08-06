namespace Flipkart;

using Flipkart.MVVM.Views;
using Flipkart.MVVM.ViewModels;
public partial class App : Application
{
	public App(AppShellViewModel shellViewModel)
	{
		InitializeComponent();
		Routing.RegisterRoute("LoginPage", typeof(LoginPage));
		Routing.RegisterRoute("ProductPage", typeof(ProductPage));
		MainPage = new AppShell(shellViewModel);
	}
}

