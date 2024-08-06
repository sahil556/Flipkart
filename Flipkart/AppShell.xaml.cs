using Flipkart.MVVM.ViewModels;
namespace Flipkart;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel shellViewModel)
	{
		InitializeComponent();
		BindingContext = shellViewModel;
	}

	public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("LoginPage");
	}
	public void cartIcon_Tapped(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MyCart/MyCart");
	}
}

