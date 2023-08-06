using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_test;

public partial class SurveyViewModel : ObservableObject
{
    private IDispatcherTimer _timer;

    public CultureInfo Culture => CultureInfo.CurrentCulture;
    public DateTime CurrentDate => DateTime.Now;

    [ObservableProperty]
    private double _currencyAmount = 1234.56;

    [ObservableProperty]
    private ObservableCollection<FruitViewModel> _fruits;

    [RelayCommand]
    private async Task Navigate(string page)
    {
        await Shell.Current.GoToAsync(page);
    }

    public SurveyViewModel()
    {
        _timer = App.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => OnPropertyChanged(nameof(CurrentDate));
        _timer.Start();

        Fruits = new ObservableCollection<FruitViewModel>()
        {
            new FruitViewModel() { Name = "LBL_APPLES" },
            new FruitViewModel() { Name = "LBL_BANANAS" },
            new FruitViewModel() { Name = "LBL_CHERRIES" },
            new FruitViewModel() { Name = "LBL_ORANGES" },
            new FruitViewModel() { Name = "LBL_PEARS" },
            new FruitViewModel() { Name = "LBL_PINEAPPLES" }
        };

        WeakReferenceMessenger.Default.Register<CultureInfo>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(CurrentDate));
            OnPropertyChanged(nameof(CurrencyAmount));
        });
    }
}
