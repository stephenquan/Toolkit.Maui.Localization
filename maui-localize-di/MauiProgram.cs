using CommunityToolkit.Maui;
using maui_localize_di.Resources.Strings;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace maui_localize_di
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            LocalizationManager.DefaultStringResource = typeof(AppStrings);
            builder.Services.AddLocalization();
            builder.Services.AddSingleton<LocalizationManager>();
            builder.Services.AddSingleton<LocalizeExtension>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}