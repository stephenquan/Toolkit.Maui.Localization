using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;

namespace maui_localize_lm;

public class StringLocalizer : ObservableObject
{
    private IStringLocalizer _localizer;

    internal StringLocalizer(IStringLocalizer localizer)
    {
        LocalizationManager.Current.CultureChanged += Current_CultureChanged;
        _localizer = localizer;
    }

    ~StringLocalizer()
    {
        LocalizationManager.Current.CultureChanged -= Current_CultureChanged;
    }

    public string this[string name] => _localizer[name];

    public string this[string name, params object[] arguments] => _localizer[name, arguments];

    private void Current_CultureChanged(object sender, EventArgs e)
    {
        OnPropertyChanged("Item");
    }
}
