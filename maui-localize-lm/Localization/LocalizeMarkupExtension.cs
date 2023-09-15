using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace maui_localize_lm.Localization;

[ContentProperty(nameof(Path))]
public class LocalizeExtension : IMarkupExtension<BindingBase>, INotifyPropertyChanged
{
    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= LocalizationManager.GetLocalizer(StringResource);

    public string Path { get; set; } = ".";
    public BindingMode Mode { get; set; } = BindingMode.OneWay;
    public IValueConverter Converter { get; set; } = null;
    public string ConverterParameter { get; set; } = null;
    public string StringFormat { get; set; } = null;
    public Type StringResource { get; set; } = null;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding($"Localizer[{Path}]", Mode, Converter, ConverterParameter, StringFormat, this);

    public LocalizeExtension()
        => LocalizationManager.Current.CurrentCultureChanged += OnCurrentCultureChanged;
    ~LocalizeExtension()
        => LocalizationManager.Current.CurrentCultureChanged -= OnCurrentCultureChanged;

    private void OnCurrentCultureChanged(object sender, CultureInfo culture)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Localizer)));

    public event PropertyChangedEventHandler PropertyChanged;
}
