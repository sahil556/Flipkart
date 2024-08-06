using CommunityToolkit.Mvvm.ComponentModel;
using Flipkart.Services;

namespace Flipkart.MVVM.ViewModels;

public partial class AppShellViewModel: ObservableObject
{
    [ObservableProperty]
    public static bool isUserLoggedIn;

    [ObservableProperty]
    public static string userName = "Login & Signup";

    public AppShellViewModel(AuthService authService)
    {
        IsUserLoggedIn = authService.IsUserLoggedIn;
        if(IsUserLoggedIn)
            UserName = "UserName";
    }
}
