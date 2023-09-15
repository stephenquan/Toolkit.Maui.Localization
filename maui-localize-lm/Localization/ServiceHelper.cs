namespace maui_localize_lm.Localization;

public static class ServiceHelper
{
    public static TService GetService<TService>()
        => Current.GetService<TService>();
    public static object? GetService(Type ServiceType)
        => Current.GetService(ServiceType);

    private static IServiceProvider Current =>
#if WINDOWS
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
#else
            null;
#endif
}