namespace Flipkart.MVVM.Views;

public partial class LocationPage : ContentPage
{
	public LocationPage()
	{
		InitializeComponent();
	}

	// Get the Last Known Location
	public async void GetCachedLocation(object sender, EventArgs e)
	{
		try{
			Location location = await Geolocation.Default.GetLastKnownLocationAsync();
			if(location != null)
			{
				 string located = $"{location.Latitude.ToString()} {location.Longitude.ToString()}";
				 await DisplayAlert("Last Location", located, "OK");
			}
		}
		catch(FeatureNotSupportedException ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
		catch(PermissionException ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
		catch(Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	// Get the Current Location
	private CancellationTokenSource _cancellationTokenSource;
	private bool _isCheckingLocation;
	public async void GetCurrentLocation(object sender, EventArgs e)
	{
		try{
			_isCheckingLocation = true;
			GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
			_cancellationTokenSource = new CancellationTokenSource();
			Location location = await Geolocation.Default.GetLocationAsync(request, _cancellationTokenSource.Token);
			if(location != null)
			{
				 string located = $"{location.Latitude.ToString()} {location.Longitude.ToString()}";
				 await DisplayAlert("Current Location", located, "OK");
			}
		}
		catch(FeatureNotSupportedException ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
		catch(PermissionException ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
		catch(Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
		finally
		{
			_isCheckingLocation = false;
		}
	}

	public void CancelRequest()
	{
		if(_isCheckingLocation && _cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
		{
			_cancellationTokenSource.Cancel();
		} 
	}

	// Start Listening for Location Changes
	private bool _isListening;
	public async void StartListening(object sender, EventArgs e)
	{
		try{
			Geolocation.LocationChanged += Geolocation_LocationChanged;
			var request = new GeolocationListeningRequest(GeolocationAccuracy.Default);
			var success = await Geolocation.StartListeningForegroundAsync(request);

			string status = success ? "Started" : "Failed";
			await DisplayAlert("Listening", status, "OK");
		}
		catch(Exception ex)
		{
			await DisplayAlert("Failed To Start Listening", ex.Message, "OK");
		}
	}

    private void Geolocation_LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
    {
        var location = e.Location;
		string located = $"{location.Latitude.ToString()} {location.Longitude.ToString()}";
		Dispatcher.DispatchAsync(() => DisplayAlert("Location Changed", located, "OK"));
    }

	async void OnStopListening(object sender, EventArgs e)
	{
		Geolocation.LocationChanged -= Geolocation_LocationChanged;
		Geolocation.StopListeningForeground();
		await DisplayAlert("Location Tracking Stopped", "Your location is no longer being tracked", "OK");
	}
}