using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_lm.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public partial class MainViewModel : ObservableObject
{
    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= LocalizationManager.GetLocalizer<AppStrings>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => Localizer["STR_CLICK_ME"],
        1 => Localizer["STR_CLICKED_1_TIME"],
        _ => Localizer["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe() => Counter++;

    [ObservableProperty]
    private List<CultureInfo> _languages = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE"),
        new CultureInfo("zh-CN"),
        new CultureInfo("ar-SA"),
    };

    [RelayCommand]
    private void ChangeLanguage(CultureInfo language)
        => LocalizationManager.Culture = language;

    public MainViewModel()
        => LocalizationManager.CultureChanged += OnCultureChanged;

    ~MainViewModel()
        => LocalizationManager.CultureChanged -= OnCultureChanged;

    private void OnCultureChanged(object sender, EventArgs e)
        => OnPropertyChanged(nameof(ClickText));
}
