namespace PostmanLibrary;

public interface IApiAccess
{
    Task<string> CallApiAsync(string url,
        bool formatResult = true, HttpAction action = HttpAction.Get);

    bool IsValidUrl(string url);
}