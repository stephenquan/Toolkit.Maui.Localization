using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_lm;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => LocalizationManager.Current.Localizer["STR_CLICK_ME"],
        1 => LocalizationManager.Current.Localizer["STR_CLICKED_1_TIME"],
        _ => LocalizationManager.Current.Localizer["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe() => Counter++;

    [ObservableProperty]
    private ObservableCollection<CultureInfo> _languages = new ObservableCollection<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE"),
        new CultureInfo("zh-CN"),
        new CultureInfo("ar-SA"),
    };

    public MainViewModel() => LocalizationManager.Current.CultureChanged += LM_CultureChanged;

    ~MainViewModel() => LocalizationManager.Current.CultureChanged -= LM_CultureChanged;

    private void LM_CultureChanged(object sender, EventArgs e) => OnPropertyChanged(nameof(ClickText));
}
