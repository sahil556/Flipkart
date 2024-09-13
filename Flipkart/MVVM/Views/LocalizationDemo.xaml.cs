
using System.Globalization;
using Flipkart.Resources.Localization;
using Flipkart;
namespace Flipkart.MVVM.Views;

public partial class LocalizationDemo : ContentPage
{
	public LocalizationResourceManager localizationResourceManager => LocalizationResourceManager.Instance;
	public LocalizationDemo()
	{
		InitializeComponent();
		BindingContext = this;
	}

	void ChangeLocale(System.Object sender, System.EventArgs e)
	{
		string str = LocalizationResourceManager.Instance["EnterEmailLabel"].ToString();
		var switchToCulture = language.Culture.TwoLetterISOLanguageName.Equals("hi", StringComparison.InvariantCultureIgnoreCase) ? 
			new CultureInfo("en-US") : new CultureInfo("hi-IN");
		
		LocalizationResourceManager.Instance.SetCulture(switchToCulture);
	}
}