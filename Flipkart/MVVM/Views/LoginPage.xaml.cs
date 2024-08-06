using Flipkart.MVVM.ViewModels;
namespace Flipkart.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}

    void Entry_Completed(System.Object sender, System.EventArgs e)
	{
		if(!string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(password.Text))
		{
			continueBtn.IsEnabled = true;
		}
		else
			continueBtn.IsEnabled = false;
	}
}