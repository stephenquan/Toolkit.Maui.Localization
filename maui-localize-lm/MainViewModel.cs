using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_lm.Resources.Strings;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_lm;

public partial class MainViewModel : ObservableObject
{
    public LocalizationManager LM => LocalizationManager.Current;
    public StringLocalizer AppStringsLocalizer => LocalizationManager.GetLocalizer<AppStrings>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => AppStringsLocalizer["STR_CLICK_ME"],
        1 => AppStringsLocalizer["STR_CLICKED_1_TIME"],
        _ => AppStringsLocalizer["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe()
    {
        Counter++;
    }

    [ObservableProperty]
    private ObservableCollection<CultureInfo> _languages = new ObservableCollection<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE"),
        new CultureInfo("zh-CN"),
        new CultureInfo("ar-SA"),
    };

    public MainViewModel()
    {
        LM.CultureChanged += LM_CultureChanged;
    }

    ~MainViewModel()
    {
        LM.CultureChanged -= LM_CultureChanged;
    }

    private void LM_CultureChanged(object sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ClickText));
    }
}
