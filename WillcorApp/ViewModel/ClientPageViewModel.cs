using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillcorApp.Models;
using WillcorApp.RestServices;

namespace WillcorApp.ViewModel
{
    public partial class ClientPageViewModel : ObservableObject
    {
        private readonly RestService _restService;

        public ObservableCollection<Client> ClientList { get; private set; } = new();

        public ObservableCollection<Client> FilteredClientList { get; private set; } = new();

        #region
        [ObservableProperty]
        public Client NewClient { get; set; } = new();

        [ObservableProperty]
        public Client SelectedClient { get; set; } = new();

        [ObservableProperty]
        private bool isMondaychecked = false;

        [ObservableProperty]
        private bool isTuesdaychecked = false;

        [ObservableProperty]
        private bool isWednesdaychecked = false;

        [ObservableProperty]
        private bool isThursdaychecked = false;

        [ObservableProperty]
        private bool isFridaychecked = false;

        [ObservableProperty]
        private bool isSaturdaychecked = false;

        [ObservableProperty]
        private bool isSundaychecked = false;

        [ObservableProperty]
        private string selectedFrequency = string.Empty;

        public int clientId;

        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        public bool showContent;

        [ObservableProperty]
        public string? searchText;
        #endregion

        public ClientPageViewModel(RestService restService)
        {
            this._restService = restService;

            _ = GetClientList();
        }

        public async Task GetClientList()
        {
            try
            {
                IsLoading = true;
                ShowContent = false;

                var clients = await _restService.GetClients();

                ClientList.Clear();

                foreach (var client in clients)
                {
                    ClientList.Add(client);
                }

                FilterClients();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load clients: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
                ShowContent = true;
            }

        }

        public async Task AddNewClient()
        {
            if (NewClient != null)
            {
                try
                {
                    IsLoading = true;
                    ShowContent = false;

                    clientId = await _restService.SaveNewClientProfile(NewClient);
                    await GetClientList();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", $"Failed to add client: {ex.Message}", "OK");
                }
                finally
                {
                    IsLoading = false;
                    ShowContent = true;

                    NewClient = new Client();
                    OnPropertyChanged(nameof(NewClient));
                }
            }
        }

        public async Task EditClient()
        {
            if(SelectedClient != null)
            {
                IsLoading = true;
                ShowContent = false;

                try
                {
                    await _restService.EditClient(SelectedClient, SelectedClient.Id);
                    await GetClientList();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", $"Failed to edit client: {ex.Message}", "OK");
                }
                finally
                {
                    IsLoading = false;
                    ShowContent = true;
                    SelectedClient = new Client();
                    OnPropertyChanged(nameof(SelectedClient));
                }


            }
        }

        public async Task AddNewPickupSchedule()
        {
            try
            {
                IsLoading = true;
                ShowContent = false;

                var collectionDays = new List<DayOfWeek>();

                if (IsMondaychecked)
                    collectionDays.Add(DayOfWeek.Monday);

                if (IsTuesdaychecked)
                    collectionDays.Add(DayOfWeek.Tuesday);

                if (IsWednesdaychecked)
                    collectionDays.Add(DayOfWeek.Wednesday);

                if (IsThursdaychecked)
                    collectionDays.Add(DayOfWeek.Thursday);

                if (IsFridaychecked)
                    collectionDays.Add(DayOfWeek.Friday);

                if (IsSaturdaychecked)
                    collectionDays.Add(DayOfWeek.Saturday);

                if (IsSundaychecked)
                    collectionDays.Add(DayOfWeek.Sunday);

                var addPickupSchedule = new AddPickupScheduleDTO
                {
                    ClientId = clientId,
                    Frequency = SelectedFrequency,
                    Destination = "string",
                    IsActive = true,
                    WeekInterval = SelectedFrequency == "Biweekly" ? 2 : 1,
                    StartDate = DateTime.UtcNow,
                    CollectionDays = collectionDays
                };

                if (SelectedFrequency == "Weekly")
                {
                    addPickupSchedule.Frequency = "Weekly";
                    addPickupSchedule.WeekInterval = 1;
                }
                else if (SelectedFrequency == "Biweekly")
                {
                    addPickupSchedule.Frequency = "Biweekly";
                    addPickupSchedule.WeekInterval = 2;
                }
                else if (SelectedFrequency == "OnCall")
                {
                    addPickupSchedule.Frequency = "OnCall";
                    addPickupSchedule.WeekInterval = 0;
                }

                await _restService.AddPickupSchedule(addPickupSchedule);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to add pickup schedule: {ex.Message}", "OK");
            }
            finally 
            {
                IsLoading = false;
                ShowContent = true;

                IsMondaychecked = false;
                IsTuesdaychecked = false;
                IsWednesdaychecked = false;
                IsThursdaychecked = false;
                IsFridaychecked = false;
                IsSaturdaychecked = false;
                IsSundaychecked = false;

                clientId = -1;

                selectedFrequency = string.Empty;

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

        public void UpdateSelectedClient()
        {
            OnPropertyChanged(nameof(SelectedClient));
        }
    }
}
