using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flipkart.Helpers;
using Flipkart.Services;
using Flipkart.Services.Models;
using Microsoft.Extensions.Logging;
namespace Flipkart.MVVM.ViewModels;

public partial class LoginPageViewModel: ObservableObject
{
    private readonly AuthService authService;
    private readonly AppShellViewModel shellViewModel;

    private readonly ILogger<LoginPageViewModel> _logger;
    public LoginPageViewModel(AuthService _authService, AppShellViewModel _shellViewModel, ILogger<LoginPageViewModel> logger)
    {
        authService = _authService;
        shellViewModel = _shellViewModel;
        _logger = logger;
        _logger.LogInformation("LoginPageViewModel Constructor");
    }

    [ObservableProperty]
    public string email;

    [ObservableProperty]
    public string password;

    [ObservableProperty]
    public bool isPasswordVisible;

    [ObservableProperty]
    public bool isBusy;

    private LoginResponse loginResponse = new LoginResponse();

    [RelayCommand]
    public async void Login()
    {
        _logger.LogInformation("Login Button Tapped");
        if(IsBusy) 
            return;
        IsBusy = true;
        loginResponse = await authService.LoginAsync(Email, Password);
        if(loginResponse != null)
        {
            await SecureStorage.Default.SetAsync("token", loginResponse.Token);
            await SecureStorage.Default.SetAsync("userid", loginResponse.UserID.ToString());
            authService.IsUserLoggedIn = true;
            shellViewModel.IsUserLoggedIn = true;
            shellViewModel.UserName = Email;
            await Shell.Current.GoToAsync("//HomePage/Home");
            var toast = Toast.Make("Login Successfully", ToastDuration.Short, 14);
            toast.Show();
            _logger.LogInformation("Login Successfully");
        }
        IsBusy = false;
        // Settings.Instance.IsUserLoggedIn = true;
    }
}
