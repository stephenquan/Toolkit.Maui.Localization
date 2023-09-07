using System.ComponentModel;

namespace maui_localize_di;

[ContentProperty(nameof(Path))]
public class FlowDirectionExtension : IMarkupExtension<BindingBase>
{
    public LocalizationManager LM { get; internal set; } = ServiceHelper.GetService<LocalizationManager>();

    public string Path { get; set; } = nameof(LocalizationManager.FlowDirection);
    public BindingMode Mode { get; set; } = BindingMode.OneWay;
    public IValueConverter Converter { get; set; } = null;
    public string ConverterParameter { get; set; } = null;
    public string StringFormat { get; set; } = null;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding(Path, Mode, Converter, ConverterParameter, StringFormat, LM);
}