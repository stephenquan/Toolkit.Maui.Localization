using System.Globalization;

namespace Toolkit.Maui.Localization.Sample;

public partial class MainPage : ContentPage
{
    public static readonly BindableProperty CountProperty
        = BindableProperty.Create(nameof(Count), typeof(int), typeof(MainPage), 0);

    public LocalizationManager LM { get; }
    public IList<CultureInfo> Cultures { get; }

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public decimal OneDollar
        => 1.0m;

    public decimal ExchangeRate
        => LM.Culture.Name switch
        {
            "en-GB" => 0.82m,
            "en-US" => 1.0m,
            "fr-FR" => 0.95m,
            "de-DE" => 0.95m,
            "zh-CN" => 7.30m,
            "ar-AE" => 3.67m,
            _ => 0.00m
        };

    public DateTime CurrentDateTime
        => DateTime.Now;

    private IDispatcherTimer _timer;

    public MainPage(LocalizationManager LM)
    {
        this.LM = LM;
        Cultures = new List<CultureInfo>(new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-AE" }.Select(s => new CultureInfo(s)));
        InitializeComponent();
        BindingContext = this;
        LM.CultureChanged += LM_CultureChanged;

        _timer = App.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(1000);
        _timer.Tick += (s, e) => OnPropertyChanged(nameof(CurrentDateTime));
        _timer.Start();
    }

    ~MainPage()
    {
        LM.CultureChanged -= LM_CultureChanged;
    }

    private void LM_CultureChanged(object sender, CultureInfo e)
    {
        OnPropertyChanged(nameof(ExchangeRate));
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        Count++;
    }
}
