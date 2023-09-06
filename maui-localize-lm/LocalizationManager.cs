using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public static class LocalizationManager
{
    public static Type DefaultStringResource;
    public static EventHandler CultureChanged;
    public static EventHandler FlowDirectionChanged;

    public static IStringLocalizer GetLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();
    public static IStringLocalizer GetLocalizer(Type StringResource)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));

    public static Type SetDefaultStringResource<TStringResource>()
        => DefaultStringResource = typeof(TStringResource);
    public static Type SetDefaultStringResource(Type StringResource)
        => DefaultStringResource = StringResource;

    public static MauiAppBuilder SetLocalizationStringResource<TStringResource>(this MauiAppBuilder builder)
    {
        SetDefaultStringResource<TStringResource>();
        return builder;
    }
    public static MauiAppBuilder SetLocalizationStringResource(this MauiAppBuilder builder, Type StringResource)
    {
        SetDefaultStringResource(StringResource);
        return builder;
    }

    public static CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (CultureInfo.CurrentCulture.Name == value.Name) return;
            CultureInfo.CurrentUICulture = value;
            CultureInfo.CurrentCulture = value;
            CultureChanged?.Invoke(null, EventArgs.Empty);
            FlowDirectionChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public static FlowDirection FlowDirection
        => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;
}
