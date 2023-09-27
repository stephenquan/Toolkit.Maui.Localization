namespace Toolkit.Maui.Localization;

public class LocalizeFlowDirectionExtension : IMarkupExtension<BindingBase>
{
    private LocalizationManager lm;
    public LocalizationManager LM
        => lm ??= ServiceHelper.GetService<LocalizationManager>();

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