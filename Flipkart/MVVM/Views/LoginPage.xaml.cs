namespace Flipkart.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}

	void Entry_Completed(System.Object sender, System.EventArgs e)
	{
		if(!string.IsNullOrEmpty(entry.Text))
		{
			continueBtn.IsEnabled = true;
		}
		else
			continueBtn.IsEnabled = false;
	}
}