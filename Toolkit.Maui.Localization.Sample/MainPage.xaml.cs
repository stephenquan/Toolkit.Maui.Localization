using System.Globalization;

namespace Toolkit.Maui.Localization.Sample;

public partial class MainPage : ContentPage
{
    public static readonly BindableProperty CountProperty
        = BindableProperty.Create(nameof(Count), typeof(int), typeof(MainPage), 0);

    public LocalizationManager LM { get; }
    public IList<CultureInfo> Cultures { get; }

    public decimal LocalMoney
        => 1.0m;

    public decimal ExchangeRate
        => LM.Culture.Name switch
        {
            "en-GB" => 1.21m,
            "en-US" => 1.0m,
            "fr-FR" => 1.06m,
            "de-DE" => 1.06m,
            "zh-CN" => 0.14m,
            "ar-AE" => 0.27m,
            _ => 0.00m
        };

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public MainPage(LocalizationManager LM)
    {
        this.LM = LM;
        Cultures = new List<CultureInfo>(new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-AE" }.Select(s => new CultureInfo(s)));
        InitializeComponent();
        BindingContext = this;
        LM.CultureChanged += LM_CultureChanged;
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
