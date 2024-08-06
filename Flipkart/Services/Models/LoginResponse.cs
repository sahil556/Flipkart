using System.Text.Json.Serialization;

namespace Flipkart.Services.Models;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("userId")]
    public int UserID { get; set; }
}
