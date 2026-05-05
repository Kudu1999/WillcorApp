using Microsoft.Extensions.Logging;
using WillcorApp.RestServices;
using WillcorApp.ViewModel;
using WillcorApp.Pages;

namespace WillcorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<RestService>();

            builder.Services.AddSingleton<PickupHistoryViewModel>();
            builder.Services.AddSingleton<TodaysListViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<PickupHistory>();
            builder.Services.AddSingleton<ClientsPage>();
            builder.Services.AddSingleton<ClientPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
