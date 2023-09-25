using Microsoft.Extensions.Logging;
using Toolkit.Maui.Localization;

namespace maui_localize_weird
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseToolkitMauiLocalization()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddLocalization();
            builder.Services.AddTransient<MainPage>();
            return builder.Build();
        }
    }
}