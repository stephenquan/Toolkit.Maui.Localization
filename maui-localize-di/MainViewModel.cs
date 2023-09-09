using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_di;

public partial class MainViewModel : ObservableObject
{
    public LocalizationManager LM { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => LM["STR_CLICK_ME"],
        1 => LM["STR_CLICKED_1_TIME"],
        _ => LM["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe()
        => Counter++;

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages;

    [RelayCommand]
    private void ChangeLanguage(CultureInfo language)
        => LM.CurrentCulture = language;

    public MainViewModel(LocalizationManager lm)
    {
        LM = lm;
        LM.CurrentCultureChanged += OnCurrentCultureChanged;
        Languages = new ObservableCollection<LanguageInfo>()
        {
            new LanguageInfo(LM, "en-US"),
            new LanguageInfo(LM, "fr-FR"),
            new LanguageInfo(LM, "de-DE"),
            new LanguageInfo(LM, "zh-CN"),
            new LanguageInfo(LM, "ar-SA"),
        };
    }

    ~MainViewModel()
    {
        LM.CurrentCultureChanged -= OnCurrentCultureChanged;
    }

    private void OnCurrentCultureChanged(object sender, CultureInfo culture)
        => OnPropertyChanged(nameof(ClickText));
}