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
    public partial class PickupHistoryViewModel : ObservableObject
    {
        private readonly RestService _restServices;

        public PickupRunDto TodayRuns { get; private set; } = new();

        public ObservableCollection<PickupRunItemDto> PickupRunItems => new ObservableCollection<PickupRunItemDto>(TodayRuns?.Items?.Where(x => x.IsCollected) ?? Enumerable.Empty<PickupRunItemDto>());

        [ObservableProperty]
        public bool isLoading;
        [ObservableProperty]
        public bool showContent;

        public PickupHistoryViewModel(RestService restServices)
        {
            _restServices = restServices;

            _= GetTodaysCompletedList();
        }

        public async Task GetTodaysCompletedList()
        {
            try
            {
                IsLoading = true;
                ShowContent = !IsLoading;

                TodayRuns = await _restServices.GetTodayRuns();
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

        public async Task EditPickup(int clientId, UpdatePickupRunItemDto updatePickup)
        {
            try
            {
                await _restServices.MarkPickupComplete(clientId, updatePickup);
                await GetTodaysCompletedList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Failed to mark pickup run as completed.", "OK");
            }
        }
    }
}
