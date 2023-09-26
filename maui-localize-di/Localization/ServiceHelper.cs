namespace maui_localize_di.Localization;

internal static class ServiceHelper
{
    public static TService GetService<TService>()
        => Current.GetService<TService>();
#nullable enable
    public static object? GetService(Type serviceType)
        => Current.GetService(serviceType);
#nullable disable

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
