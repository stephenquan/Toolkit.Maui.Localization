using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_ext.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_ext;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private IStringLocalizer _localizer = ServiceHelper.GetService<IStringLocalizer<AppStrings>>();

    public FlowDirection FlowDirection => Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

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
            OnPropertyChanged(nameof(Localizer));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged(nameof(ClickText));
        }
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
}
