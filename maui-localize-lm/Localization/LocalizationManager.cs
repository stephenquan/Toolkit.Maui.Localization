using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace maui_localize_lm.Localization;

public class LocalizationManager : INotifyPropertyChanged
{
    public static Type DefaultStringResource;
    public static IStringLocalizer GetLocalizer(Type StringResource = null)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));
    public static IStringLocalizer GetStringLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();

    public static LocalizationManager _current;
    public static LocalizationManager Current
        => _current ??= new LocalizationManager();

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
            if (CultureInfo.CurrentCulture.Name == value.Name)
            {
                return;
            }
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CurrentCultureChanged?.Invoke(null, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
            FlowDirectionChanged?.Invoke(this, FlowDirection);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }
    }

    public string this[string name] => DefaultLocalizer[name];
    public string this[string name, params object[] args] => DefaultLocalizer[name, args];

    public event PropertyChangedEventHandler PropertyChanged;
}
