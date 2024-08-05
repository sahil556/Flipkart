using System.Timers;
using System;
using global::Flipkart.MVVM.ViewModels;

namespace Flipkart.MVVM.Views;
public partial class HomePage : ContentPage
{
	private readonly System.Timers.Timer timer;

	private readonly HomePageViewModel homePageViewModel;
	private int Index {get; set;}
	public HomePage(HomePageViewModel hpvm)
	{
		InitializeComponent();
		Index = 0;
		BindingContext = homePageViewModel = hpvm;
		timer = new System.Timers.Timer(3000);
		timer.Elapsed += OnTimeElapsed;
		timer.Start();
	}

	private void OnTimeElapsed(object sender, ElapsedEventArgs e)
	{
		MainThread.BeginInvokeOnMainThread(() => {
			CarouselView.Position = (CarouselView.Position + 1) % homePageViewModel.CarouselOptions.Count;
			CarouselView.ScrollTo(CarouselView.Position);
		});
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		timer.Dispose();
    }
}
