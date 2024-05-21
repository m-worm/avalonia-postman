using System.Text;
using System.Text.Json;

namespace PostmanLibrary;

public class ApiAccess : IApiAccess
{
    private readonly HttpClient _client = new();

    public async Task<string> CallApiAsync(
        string url,
        string content,
        HttpAction action = HttpAction.Get,
        bool formatResult = true)

    {
        StringContent stringContent = new(content, Encoding.UTF8, "application/json");     
        return await CallApiAsync(url, stringContent, action, formatResult);
    }

    public async Task<string> CallApiAsync(
        string url,
        HttpContent? content = null,
        HttpAction action = HttpAction.Get,
        bool formatResult = true)
    {
        HttpResponseMessage? response;

        switch (action)
        {
            case HttpAction.Get:
                response = await _client.GetAsync(url);
                break;
            case HttpAction.Post:
                response = await _client.PostAsync(url, content);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
        
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            if (formatResult)
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
                json = JsonSerializer.Serialize(jsonElement,
                    new JsonSerializerOptions { WriteIndented = true });
            }

            return json;
        }
        else
        {
            return $"Error: {response.StatusCode}";
        }
    }

    public bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }

        bool result = Uri.TryCreate(url, UriKind.Absolute, out var uri) && 
                      (uri.Scheme == Uri.UriSchemeHttps);

        return result;
    }
}

