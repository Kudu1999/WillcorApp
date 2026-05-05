using System;
using WillcorApp.Models;
using WillcorApp.ViewModel;

namespace WillcorApp
{
    public partial class MainPage : ContentPage
    {
        private readonly TodaysListViewModel _todaysListViewModel;

        public bool showConfirmPickup = false;
        int smallbags = 0;
        int bigbags = 0;
        double trailer = 0;
        int itemID = -1;

        public MainPage(TodaysListViewModel todaysListViewModel)
        {
            InitializeComponent();
            _todaysListViewModel = todaysListViewModel ?? throw new ArgumentNullException(nameof(todaysListViewModel));
            BindingContext = _todaysListViewModel;
        }



        private async void AddClientTapped(object sender, TappedEventArgs e)
        {

            addClientPopup.IsVisible = true;

            await _todaysListViewModel.GetClientList();
        }

        private async void AddSelectedClientsClicked(object sender, EventArgs e)
        {
            if (_todaysListViewModel.SelectedClients.Count == 0)
            {
                await DisplayAlert("No Clients Selected", "Please select at least one client to add to the pickup run.", "OK");
                return;
            }

            await _todaysListViewModel.AddSelectedClientsToRun();
            await _todaysListViewModel.GetTodaysList();

            addClientPopup.IsVisible = false;

        }

        private void CancelAddClientsClicked(object sender, EventArgs e)
        {
            addClientPopup.IsVisible = false;
        }

        private void MarkCompleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button?.BindingContext is PickupRunItemDto run)
            {
                var clientName = run.ClientName;
                itemID = run.Id;

                PickupClientName.Text = clientName;

                // Now you can use it
                showConfirmPickup = true;
                pickupPopup.IsVisible = showConfirmPickup;
                addClientTOTodayBtn.IsEnabled = false;

                // Example: announce with name
                SemanticScreenReader.Announce($"Pickup confirmation for {clientName} is now visible.");
            }
        }

        private async void ConfirmPickupClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var updatePickup = new UpdatePickupRunItemDto
            {
                IsCollected = true,
                SmallBagsCollected = smallbags,
                BigBagsCollected = bigbags,
                BagsCollected = bigbags + smallbags,
                TrailerLoadsCollected = trailer,
                Notes = pickupNotes.Text
            };

            await _todaysListViewModel.MarkPickupCompleted(itemID, updatePickup);

            showConfirmPickup = false;
            pickupPopup.IsVisible = showConfirmPickup;
            addClientTOTodayBtn.IsEnabled = true;

            smallbags = 0;
            bigbags = 0;
            trailer = 0;
            itemID = -1;

            smallbagstxt.Text = smallbags.ToString();
            bigbagstxt.Text = bigbags.ToString();
            trailerloadstxt.Text = trailer.ToString();
        }

        private async void SkippedPickupClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button?.BindingContext is PickupRunItemDto run)
            {
                itemID = run.Id;
            }

            var updatePickup = new UpdatePickupRunItemDto
            {
                IsCollected = true,
                SmallBagsCollected = 0,
                BigBagsCollected = 0,
                BagsCollected = 0,
                TrailerLoadsCollected = 0,
                Notes = pickupNotes.Text
            };

            await _todaysListViewModel.MarkPickupCompleted(itemID, updatePickup);

            itemID = -1;

        }

        private void CancelPickupClicked(object sender, EventArgs e)
        {
            showConfirmPickup = false;
            pickupPopup.IsVisible = showConfirmPickup;
            addClientTOTodayBtn.IsEnabled = true;

            smallbags = 0;
            bigbags = 0;
            trailer = 0;

            smallbagstxt.Text = smallbags.ToString();
            bigbagstxt.Text = smallbags.ToString();
            trailerloadstxt.Text = smallbags.ToString();
        }

        private void Addsmallbag(object sender, EventArgs e)
        {
            smallbags += 1;
            smallbagstxt.Text = smallbags.ToString();
        }
        private void AddBigbag(object sender, EventArgs e)
        {
            bigbags += 1;
            bigbagstxt.Text = bigbags.ToString();
        }
        private void AddTrailer(object sender, EventArgs e)
        {
            trailer = trailer + 0.25;
            trailerloadstxt.Text = trailer.ToString();
        }

        private void Subsmallbag(object sender, EventArgs e)
        {
            smallbags -= 1;
            smallbagstxt.Text = smallbags.ToString();
        }
        private void SubBigbag(object sender, EventArgs e)
        {
            bigbags -= 1;
            bigbagstxt.Text = bigbags.ToString();
        }
        private void SubTrailer(object sender, EventArgs e)
        {
            trailer = trailer - 0.25;
            trailerloadstxt.Text = trailer.ToString();
        }
    }
}
