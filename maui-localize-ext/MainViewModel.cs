using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_ext.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_ext;

public partial class MainViewModel : ObservableObject
{
    private IStringLocalizer _localizer = ServiceHelper.GetService<IStringLocalizer<AppStrings>>();

    public string TITLE_MAIN => _localizer["TITLE_MAIN"];
    public string LBL_HELLO => _localizer["LBL_HELLO"];
    public string LBL_WELCOME => _localizer["LBL_WELCOME"];


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;
    public string ClickText => Counter switch
    {
        0 => _localizer["STR_CLICK_ME"],
        1 => _localizer["STR_CLICKED_1_TIME"],
        _ => _localizer["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe()
    {
        Counter++;
    }

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null)
            {
                return;
            }
            CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(TITLE_MAIN));
            OnPropertyChanged(nameof(LBL_HELLO));
            OnPropertyChanged(nameof(LBL_WELCOME));
            OnPropertyChanged(nameof(ClickText));
        }
    }

    [ObservableProperty]
    private ObservableCollection<CultureInfo> _languages = new ObservableCollection<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE"),
        new CultureInfo("zh-CN")
    };
}
