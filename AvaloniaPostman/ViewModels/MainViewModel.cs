// The skeleton for this code was generated using ChatGPT4 using the XAML view as input

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AvaloniaPostman.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // Property to bind with the API TextBox
    [ObservableProperty] private string apiUrl;

    // Property to bind with the Results TextBox
    [ObservableProperty] private string results;

    // Property to bind with the Status TextBox
    [ObservableProperty] private string status = "Ready";

    // ICommand for the Go button
    [RelayCommand]
    private async Task ExecuteGoAsync()
    {
        if (!string.IsNullOrWhiteSpace(ApiUrl))
        {
            try
            {
                // Simulate an API call
                Status = "Loading...";
                await Task.Delay(1000); // Simulate API call delay
                Results = $"Results from: {ApiUrl}";
            }
            catch (System.Exception ex)
            {
                Results = $"Error: {ex.Message}";
            }
            finally
            {
                Status = "Ready";
            }
        }
    }
}