using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace maui_localize_lm;

public class LocalizationManager : INotifyPropertyChanged
{
    public static Type DefaultStringResource;
    public static IStringLocalizer GetStringLocalizer(Type StringResource = null)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));
    public static IStringLocalizer GetStringLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();

    public static LocalizationManager _current;
    public static LocalizationManager Current
        => _current ??= new LocalizationManager();

    public static EventHandler<CultureInfo> CurrentCultureChanged;
    public static EventHandler<FlowDirection> FlowDirectionChanged;


    public FlowDirection FlowDirection
        => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;

    public CultureInfo CurrentCulture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (CultureInfo.CurrentCulture.Name == value.Name)
            {
                return;
            }
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CurrentCultureChanged?.Invoke(null, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
            FlowDirectionChanged?.Invoke(this, FlowDirection);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
