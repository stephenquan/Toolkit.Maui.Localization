namespace Toolkit.Maui.Localization;

public static class LocalizationAppBuilderExtension
{
    public static MauiAppBuilder UseToolkitMauiLocalization<TStringResource>(this MauiAppBuilder builder)
    {
        LocalizationManager.DefaultStringResource = typeof(TStringResource);
        builder.Services.AddSingleton<LocalizationManager>();
        builder.Services.AddLocalization();
        return builder;
    }
}
