using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillcorApp.Models;
using WillcorApp.RestServices;

namespace WillcorApp.ViewModel
{
    public partial class TodaysListViewModel : ObservableObject
    {
        private readonly RestService restService;
        public PickupRunDto TodayRuns { get; private set; } = new();
        public ObservableCollection<Client> ClientList { get; private set; } = new();
        public ObservableCollection<Client> FilteredClientList { get; private set; } = new();
        public ObservableCollection<object> SelectedClients { get; set; } = new();

        public ObservableCollection<PickupRunItemDto> PickupRunItems => new ObservableCollection<PickupRunItemDto>(TodayRuns?.Items?.Where(x => !x.IsCollected) ?? Enumerable.Empty<PickupRunItemDto>());

        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        public bool isLoadingClients;

        [ObservableProperty]
        public bool showContent;

        [ObservableProperty]
        public bool showContentClients;

        [ObservableProperty]
        public string? searchText;

        public TodaysListViewModel(RestService restService)
        {
            this.restService = restService;
            _ = GetTodaysList();
        }

        public async Task GetTodaysList()
        {
            try
            {
                IsLoading = true;
                ShowContent = !IsLoading;

                TodayRuns = await restService.GetTodayRuns();
                OnPropertyChanged(nameof(TodayRuns));
                OnPropertyChanged(nameof(PickupRunItems));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Failed to get list of runs", "Ok");
            }
            finally
            {
                IsLoading = false;
                ShowContent = !IsLoading;
            }


            
        }

        public async Task GetClientList()
        {
            try
            {
                IsLoadingClients = true;
                ShowContentClients = !IsLoadingClients;

                var clients = await restService.GetClients();

                ClientList.Clear();

                foreach (var client in clients)
                {
                    ClientList.Add(client);
                }

                FilterClients();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Failed to get client information", "Ok");
                
            }
            finally
            {
                IsLoadingClients = false;
                ShowContentClients = !IsLoadingClients;
            }
        }
        public async Task AddSelectedClientsToRun()
        {
            try
            {
                foreach (var client in SelectedClients.OfType<Client>())
                {
                    var addPickup = new AddPickup
                    {
                        ClientId = client.Id,
                        Destination = client.Address,
                        Notes = ""
                    };
                    await restService.AddExtraPickup(TodayRuns.Id, addPickup);
                }
                await Shell.Current.DisplayAlert("Success", "Selected clients added to the pickup run.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Failed to add selected clients to the pickup run.", "OK");
            }
        }

        public async Task RefreshData()
        {
            await GetTodaysList();
            await GetClientList();
        }

        public async Task MarkPickupCompleted(int clientId, UpdatePickupRunItemDto updatePickup)
        {
            try
            {
                await restService.MarkPickupComplete(clientId, updatePickup);
                await RefreshData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Failed to mark pickup run as completed.", "OK");
            }
        }
        partial void OnSearchTextChanged(string? value)
        {
            FilterClients();
        }

        private void FilterClients()
        {
            FilteredClientList.Clear();

            var search = SearchText?.Trim().ToLower();

            var filtered = string.IsNullOrWhiteSpace(search)
                ? ClientList
                : ClientList.Where(x =>
                    (x.Name?.ToLower().Contains(search) ?? false) ||
                    (x.AreaCode?.ToLower().Contains(search) ?? false) ||
                    (x.ReferenceNumber?.ToLower().Contains(search) ?? false) ||
                    (x.Address?.ToLower().Contains(search) ?? false));

            foreach (var client in filtered)
            {
                FilteredClientList.Add(client);
            }
        }
    }
}
