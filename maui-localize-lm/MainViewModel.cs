using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_lm.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_lm;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void ClickMe()
        => Counter++;

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>(
        new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-SA" }
        .Select(s => new LanguageInfo(s)));

    [RelayCommand]
    private void ChangeLanguage(LanguageInfo language)
        => LocalizationManager.Current.CurrentCulture = language;
}
