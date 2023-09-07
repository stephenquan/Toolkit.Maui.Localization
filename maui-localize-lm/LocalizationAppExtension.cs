﻿namespace maui_localize_lm;

public static class LocalizationAppExtension
{
    public static MauiAppBuilder UseLocalizationManager<TStringResource>(this MauiAppBuilder builder)
        => UseLocalizationManager(builder, typeof(TStringResource));

    public static MauiAppBuilder UseLocalizationManager(this MauiAppBuilder builder, Type StringResource = null)
    {
        LocalizationManager.DefaultStringResource = StringResource;
        builder.Services.AddLocalization();
        return builder;
    }
}
