using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.Helpers;
using Flipkart.Services;
using Flipkart.Services.Models;
namespace Flipkart.MVVM.ViewModels;

public partial class LoginPageViewModel: ObservableObject
{
    private readonly AuthService authService;
    private readonly AppShellViewModel shellViewModel;
    public LoginPageViewModel(AuthService _authService, AppShellViewModel _shellViewModel)
    {
        authService = _authService;
        shellViewModel = _shellViewModel;
    }

    [ObservableProperty]
    public string email;

    [ObservableProperty]
    public string password;

    [ObservableProperty]
    public bool isPasswordVisible;

    private LoginResponse loginResponse = new LoginResponse();

    [RelayCommand]
    public async void Login()
    {
        loginResponse = await authService.LoginAsync(Email, Password);
        if(loginResponse != null)
        {
            await SecureStorage.Default.SetAsync("token", loginResponse.Token);
            authService.IsUserLoggedIn = true;
            shellViewModel.IsUserLoggedIn = true;
            shellViewModel.UserName = Email;
            await Shell.Current.GoToAsync("//HomePage/Home");
        }
        // Settings.Instance.IsUserLoggedIn = true;
    }
}
