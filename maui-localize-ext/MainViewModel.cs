using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_ext.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_ext;

public partial class MainViewModel : ObservableObject
{
    public static EventHandler CultureChanged;

    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= ServiceHelper.GetService<IStringLocalizer<AppStrings>>();

    public FlowDirection FlowDirection
        => Culture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;

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
    private void ClickMe()
        => Counter++;

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null) return;
            if (value.Name == CultureInfo.CurrentUICulture.Name) return;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(Localizer));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged(nameof(ClickText));
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>()
    {
        new LanguageInfo("en-US"),
        new LanguageInfo("fr-FR"),
        new LanguageInfo("de-DE"),
        new LanguageInfo("zh-CN"),
        new LanguageInfo("ar-SA"),
    };

    [RelayCommand]
    private void ChangeLanguage(CultureInfo language)
        => Culture = language;
}
