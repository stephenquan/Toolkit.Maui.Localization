using System.Text.RegularExpressions;

namespace maui_localize_lm;

[ContentProperty(nameof(Path))]
public class LocalizeExtension : IMarkupExtension<BindingBase>
{
    public string Path { get; set; } = ".";
    public BindingMode Mode { get; set; } = BindingMode.OneWay;
    public IValueConverter Converter { get; set; } = null;
    public string ConverterParameter { get; set; } = null;
    public string StringFormat { get; set; } = null;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);

    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding(
            Regex.IsMatch(Path, @"(^FlowDirection$|^Culture\.?)") ? Path : $"[{Path}]",
            Mode, Converter, ConverterParameter, StringFormat, LocalizationManager.Current);
}
