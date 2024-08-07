using Flipkart.MVVM.Views;
using Flipkart.MVVM.ViewModels;

using Microsoft.Extensions.Logging;
#if __ANDROID__
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Content;
#endif
using Flipkart.Services;
using CommunityToolkit.Maui;
namespace Flipkart;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fontello.ttf", "Icons");
                fonts.AddFont("Riona.ttf", "Riona");

            });
			
		builder.Services.AddTransient<HomePageViewModel>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<ProductPageViewModel>();
		builder.Services.AddTransient<ProductPage>();
		builder.Services.AddTransient<MyCartViewModel>();
		builder.Services.AddTransient<MyCart>();
		builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddSingleton<RestService>();
		builder.Services.AddSingleton<AuthService>();
		builder.Services.AddSingleton<AppShellViewModel>();
		builder.Services.AddSingleton<ProductService>();
		builder.Services.AddSingleton<CartService>();


#if __ANDROID__
		builder.Services.AddTransient<INotificationManagerService, Flipkart.Platforms.Android.NotificationManagerService>();
		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        });
#endif
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

