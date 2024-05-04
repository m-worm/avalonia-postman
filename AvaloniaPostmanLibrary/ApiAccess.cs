using System.Text.Json;

namespace PostmanLibrary;

public class ApiAccess : IApiAccess
{
    private readonly HttpClient _client = new();

    public async Task<string> CallApiAsync(string url,
        bool formatResult = true, HttpAction action = HttpAction.Get)
    {
        var response = await _client.GetAsync(url);

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

