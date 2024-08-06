using CommunityToolkit.Mvvm.ComponentModel;

namespace Flipkart.MVVM.ViewModels;

public partial class AppShellViewModel: ObservableObject
{
    [ObservableProperty]
    public static bool isUserLoggedIn;

    [ObservableProperty]
    public static string userName = "Login & Signup";
}
