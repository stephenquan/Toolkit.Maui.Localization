using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace maui_localize_di;

public class LocalizationManager : INotifyPropertyChanged
{
    public static Type DefaultStringResource;
    public static IStringLocalizer GetStringLocalizer(Type StringResource = null)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));
    public static IStringLocalizer GetStringLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();

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
            if (CultureInfo.CurrentUICulture.Name == value.Name)
            {
                return;
            }
            FlowDirection _flowDirection = this.FlowDirection;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CurrentCultureChanged?.Invoke(this, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
            FlowDirectionChanged?.Invoke(this, FlowDirection);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
