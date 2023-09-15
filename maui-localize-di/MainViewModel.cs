using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_di.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_di;

public partial class MainViewModel : ObservableObject
{
    public LocalizationManager LM { get; set; }

    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void ClickMe()
        => Counter++;

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>(
        new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-SA" }
        .Select(s => new LanguageInfo(s)));

    public MainViewModel(LocalizationManager LM)
    {
        this.LM = LM;
    }

    [RelayCommand]
    private void ChangeLanguage(LanguageInfo language)
        => LM.CurrentCulture = language;
}