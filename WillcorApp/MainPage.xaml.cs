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

        public MainPage(TodaysListViewModel todaysListViewModel)
        {
            InitializeComponent();
            BindingContext = todaysListViewModel;
        }



        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void MarkCompleteClicked(object sender, EventArgs e)
        {
            showConfirmPickup = true;
            pickupPopup.IsVisible = showConfirmPickup;
            SemanticScreenReader.Announce("Pickup confirmation popup is now visible.");
        }

        private void Button_Clicked(object sender, EventArgs e)
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
    }
}
