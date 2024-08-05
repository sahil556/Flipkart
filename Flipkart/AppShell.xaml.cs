namespace Flipkart;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

	public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("LoginPage");
	}
}

