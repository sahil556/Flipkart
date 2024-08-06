namespace Flipkart.Helpers;

public class Settings
{
    public static Settings Instance  = new Settings();
    public bool IsUserLoggedIn
    {
        get => Preferences.Get("IsUserLoggedIn", false);
        set => Preferences.Set("IsUserLoggedIn", value);
    }
}
