using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public partial class LocalizationManager : ObservableObject
{
    private static LocalizationManager _current = new LocalizationManager();
    public static LocalizationManager Current => _current;

    public event EventHandler CultureChanged;

    public FlowDirection FlowDirection => Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

    private IStringLocalizer _localizer;

    public void SetDefaultLocalizer<T>()
    {
        _localizer = ServiceHelper.GetService<IStringLocalizer<T>>();
    }

    public string this[string name] => _localizer[name];

    public string this[string name, params object[] arguments] => _localizer[name, arguments];

    static public StringLocalizer GetLocalizer<T>() => new StringLocalizer(ServiceHelper.GetService<IStringLocalizer<T>>());

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null) return;
            CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged("Item");
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
