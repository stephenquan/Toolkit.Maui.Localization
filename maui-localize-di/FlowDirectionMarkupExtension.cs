using System.ComponentModel;

namespace maui_localize_di;

[ContentProperty(nameof(Path))]
public class FlowDirectionExtension : IMarkupExtension<BindingBase>, INotifyPropertyChanged
{
    private LocalizationManager lm;
    public LocalizationManager LM
        => lm ??= ServiceHelper.GetService<LocalizationManager>();

    public FlowDirection FlowDirection
        => LM.FlowDirection;

    public BindingMode Mode { get; set; } = BindingMode.OneWay;
    public IValueConverter Converter { get; set; } = null;
    public string ConverterParameter { get; set; } = null;
    public string StringFormat { get; set; } = null;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding($"FlowDirection", Mode, Converter, ConverterParameter, StringFormat, this);

    public FlowDirectionExtension()
        => LocalizationManager.FlowDirectionChanged += OnFlowDirectionChanged;
    ~FlowDirectionExtension()
        => LocalizationManager.FlowDirectionChanged -= OnFlowDirectionChanged;
    private void OnFlowDirectionChanged(object sender, FlowDirection e)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));

    public event PropertyChangedEventHandler PropertyChanged;
}