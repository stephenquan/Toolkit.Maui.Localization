using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_test;

public partial class MainViewModel : ObservableObject
{
    public CultureInfo Culture => CultureInfo.CurrentCulture;
    public string TITLE_MAIN => Resources.Strings.AppStrings.TITLE_MAIN;
    public string LBL_WELCOME => Resources.Strings.AppStrings.LBL_WELCOME;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => Resources.Strings.AppStrings.STR_CLICK_ME,
        1 => Resources.Strings.AppStrings.STR_CLICK_ONE_TIME,
        _ => string.Format(Resources.Strings.AppStrings.STR_CLICK_N_TIMES, Counter)
    };

    [RelayCommand]
    private void ClickMe()
    {
        Counter++;
    }

    [RelayCommand]
    private async Task Navigate(string page)
    {
        await Shell.Current.GoToAsync(page);
    }

    public MainViewModel()
    {
        WeakReferenceMessenger.Default.Register<CultureInfo>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(TITLE_MAIN));
            OnPropertyChanged(nameof(LBL_WELCOME));
            OnPropertyChanged(nameof(ClickText));
        });
    }
}
