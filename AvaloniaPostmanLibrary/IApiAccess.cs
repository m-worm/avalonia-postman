namespace PostmanLibrary;

public interface IApiAccess
{
    Task<string> CallApiAsync(
        string url,
        string content,
        HttpAction action = HttpAction.Get,
        bool formatResult = true);

    Task<string> CallApiAsync(
        string url,
        HttpContent? content = null,
        HttpAction action = HttpAction.Get,
        bool formatResult = true);

    bool IsValidUrl(string url);
}