
using System.Text.Json;
using Flipkart.MVVM.Models;
using Flipkart.Services.Models;
using Microsoft.Extensions.Logging;

namespace Flipkart.Services;
public class AuthService: RestService
{
    // declare logger
    private readonly ILogger<AuthService> _logger;

    // initialize _logger in constructor
    public AuthService(ILogger<AuthService> logger)
    {
        _logger = logger;
        _logger.LogInformation("AuthService Constructor");
    }
    public bool IsUserLoggedIn
    {
        get
        {
            var token = SecureStorage.GetAsync("token").Result;
            return !string.IsNullOrEmpty(token);
        }
        set
        {
            if(!value)
            {
                SecureStorage.Remove("token");
                SecureStorage.Remove("userId");
            }
        }
    }

    public async Task<LoginResponse> LoginAsync(string username, string password)
    {
        var payload = new { username, password };
        var response =  await PostAsync<LoginResponse>("auth/login", payload);
        _logger.LogInformation("Login Request Received");
        try{
            var userResponse = await client.GetAsync("users");
            userResponse.EnsureSuccessStatusCode();
            var userResponseContent = await userResponse.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<User>>(userResponseContent);

                var user = users.FirstOrDefault(x => x.UserName == username);
                if(user != null)
                {
                    response.UserID = user.Id;
                }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error fetching user details: {ex.Message}");
            _logger.LogError("Error fetching user details: {0}", ex.Message);
        }
        return response;
    }
}
