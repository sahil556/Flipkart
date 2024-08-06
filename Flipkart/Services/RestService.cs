using System.Text;
using System.Text.Json;

namespace Flipkart.Services;

public class RestService
{
    protected HttpClient client;

    JsonSerializerOptions options;

    public RestService()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri("https://fakestoreapi.com/")
        };
        options = new JsonSerializerOptions { WriteIndented = true };
    }

    protected async Task<T> GetAsync<T>(string endpoint)
    {
        if (!IsInternetAvailable())
            return default;

        try{
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            using(var responseContent = await response.Content.ReadAsStreamAsync())
            {
                return JsonSerializer.Deserialize<T>(responseContent, options);
            }
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Unable to retrieve data", "Ok");
            return default;
        }
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        if (!IsInternetAvailable())
            return null;
        
        try{
            return await client.DeleteAsync(endpoint);
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Unable to delete data", "Ok");
            return null;
        }
    }

    protected async Task<T> PostAsync<T>(string endpoint, object payload)
    {
        if (!IsInternetAvailable())
            return default;
        try
        {
            string json = JsonSerializer.Serialize(payload);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            using(var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return JsonSerializer.Deserialize<T>(responseStream);
            }
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Unable to send data", "Ok");
            return default;
        }
    }

    private bool IsInternetAvailable()
    {
        NetworkAccess accessType = Connectivity.NetworkAccess;
        if (accessType != NetworkAccess.Internet)
        {
            if (Shell.Current != null)
            {
                if(accessType == NetworkAccess.ConstrainedInternet)
                    Shell.Current.DisplayAlert("Error", "Internet Access is restricted", "Ok");
                else
                    Shell.Current.DisplayAlert("Error", "No Internet Connection", "Ok");
            }
            return false;
        }
        return true;
    }
}
