using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace maui_localize_di;

public class LocalizationManager : INotifyPropertyChanged
{
    public static Type DefaultStringResource;
    public static IStringLocalizer GetLocalizer(Type StringResource = null)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));
    public static IStringLocalizer GetLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();

    public EventHandler<CultureInfo> CurrentCultureChanged;
    public EventHandler<FlowDirection> FlowDirectionChanged;

    private IStringLocalizer _defaultLocalizer;
    private IStringLocalizer DefaultLocalizer
        => _defaultLocalizer ??= GetLocalizer(DefaultStringResource);

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
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CurrentCultureChanged?.Invoke(this, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
            FlowDirectionChanged?.Invoke(this, FlowDirection);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));
        }
    }

    public string this[string name] => DefaultLocalizer[name];
    public string this[string name, params object[] args] => DefaultLocalizer[name, args];

    public CultureInfo InstalledCulture
        => CultureInfo.InstalledUICulture;

    public LocalizationManager()
    {
    }


    public event PropertyChangedEventHandler PropertyChanged;
}
