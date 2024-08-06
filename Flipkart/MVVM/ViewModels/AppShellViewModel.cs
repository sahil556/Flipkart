using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.Services;

namespace Flipkart.MVVM.ViewModels;

public partial class AppShellViewModel: ObservableObject
{
    [ObservableProperty]
    public static bool isUserLoggedIn;

    [ObservableProperty]
    public static string userName = "Login & Signup";

    private AuthService authService;

    public AppShellViewModel(AuthService authService)
    {
        this.authService = authService;
        IsUserLoggedIn = authService.IsUserLoggedIn;
        if(IsUserLoggedIn)
            UserName = "UserName";
    }

    [RelayCommand]
    public void Logout()
    {
        SecureStorage.Default.RemoveAll();
        authService.IsUserLoggedIn = IsUserLoggedIn = false;
        UserName = "Login & Signup";
        Shell.Current.FlyoutIsPresented = false;
    }
}
