namespace WillcorApp.Pages;

using WillcorApp.Models;
using WillcorApp.ViewModel;

public partial class PickupHistory : ContentPage
{
	private readonly PickupHistoryViewModel _viewModel;

    public bool showConfirmPickup = false;
    int? smallbags = 0;
    int? bigbags = 0;
    double? trailer = 0;
    int itemID = -1;
    string? note = "";

    public PickupHistory(PickupHistoryViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		BindingContext = _viewModel;
	}

	private async void Editbutton_Clicked(object sender, EventArgs e)
	{
        var button = sender as Button;

        if (button?.BindingContext is PickupRunItemDto run)
        {
            var clientName = run.ClientName;
            itemID = run.Id;
            smallbags = run.SmallBagsCollected;
            bigbags = run.BigBagsCollected;
            trailer = run.TrailerLoadsCollected;
            note = run.Notes;

            smallbagstxt.Text = smallbags.ToString();
            bigbagstxt.Text = bigbags.ToString();
            trailerloadstxt.Text = trailer.ToString();
            pickupNotes.Text = note?.ToString() ?? "";


            PickupClientName.Text = clientName;

            // Now you can use it
            showConfirmPickup = true;
            pickupPopup.IsVisible = showConfirmPickup;

            // Example: announce with name
            SemanticScreenReader.Announce($"Pickup confirmation for {clientName} is now visible.");
        }
    }

    private async void ConfirmEditPickupClicked(object sender, EventArgs e)
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

        await _viewModel.EditPickup(itemID, updatePickup);

        showConfirmPickup = false;
        pickupPopup.IsVisible = showConfirmPickup;

        smallbags = 0;
        bigbags = 0;
        trailer = 0;

        smallbagstxt.Text = smallbags.ToString();
        bigbagstxt.Text = smallbags.ToString();
        trailerloadstxt.Text = smallbags.ToString();
    }

    private void CancelEditPickupClicked(object sender, EventArgs e)
    {
        showConfirmPickup = false;
        pickupPopup.IsVisible = showConfirmPickup;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.GetTodaysCompletedList();
    }
}