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
    public partial class TodaysListViewModel : ObservableObject
    {
        private readonly RestService restService;
        public PickupRunDto TodayRuns { get; private set; } = new();

        [ObservableProperty]
        public bool isLoading;

        public TodaysListViewModel(RestService restService) 
        {
            this.restService = restService;
            _ = GetTodaysList();
        }

        async Task GetTodaysList()
        {
            try
            {
                IsLoading = true;

                TodayRuns = await restService.GetTodayRuns();
                OnPropertyChanged(nameof(TodayRuns));


            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to get list of runs", "Ok");
            }
            finally 
            {
                IsLoading = false;
            }
        }
    }
}
