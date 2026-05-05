using WillcorApp.Models;
using WillcorApp.ViewModel;

namespace WillcorApp.Pages;

public partial class ClientsPage : ContentPage
{
    private readonly ClientPageViewModel _clientPageViewModel;

    private bool AddScheduleToClient = false;

    public ClientsPage(ClientPageViewModel clientPageViewModel)
    {
        InitializeComponent();
        _clientPageViewModel = clientPageViewModel ?? throw new ArgumentNullException(nameof(clientPageViewModel));
        BindingContext = _clientPageViewModel;
    }

    private async void AddNewClient(object sender, EventArgs e)
    {
        AddNewClientUI.IsVisible = true;
        ClientListUI.IsVisible = false;
    }

    private async void EditClient(object sender, EventArgs e)
    {
        await _clientPageViewModel.EditClient();

        EditClientUI.IsVisible = false;
        ClientListUI.IsVisible = true;
    }

    private async void OpenEditClientUI(object sender, EventArgs e)
    {
        EditClientUI.IsVisible = true;
        ClientListUI.IsVisible = false;

        var client = (sender as Button)?.BindingContext as Client;
        if (client != null)
        {
            _clientPageViewModel.SelectedClient = client;
            _clientPageViewModel.UpdateSelectedClient();
        }
    }

    private async void CloseEditClientUI(object sender, EventArgs e)
    {
        EditClientUI.IsVisible = false;
        ClientListUI.IsVisible = true;

        _clientPageViewModel.SelectedClient = new Client();
        _clientPageViewModel.UpdateSelectedClient();
    }



    private async void CloseNewClient(object sender, EventArgs e)
    {
        AddNewClientUI.IsVisible = false;
        ClientListUI.IsVisible = true;
    }

    private async void SaveNewClient(object senderr, EventArgs e)
    {
        await _clientPageViewModel.AddNewClient();

        if (AddScheduleToClient)
        {
            await _clientPageViewModel.AddNewPickupSchedule();
        }

        AddNewClientUI.IsVisible = false;
        ClientListUI.IsVisible = true;

    }

    private async void ShowSchedule(object sender, EventArgs e)
    {
        ScheduleInputUI.IsVisible = true;
        AddScheduleBTN.IsVisible = false;
        CloseScheduleBTN.IsVisible = true;

        AddScheduleToClient = true;
    }

    private async void CloseSchedule(object sender, EventArgs e)
    {
        ScheduleInputUI.IsVisible = false;
        AddScheduleBTN.IsVisible = true;
        CloseScheduleBTN.IsVisible = false;

        AddScheduleToClient = false;
    }
}