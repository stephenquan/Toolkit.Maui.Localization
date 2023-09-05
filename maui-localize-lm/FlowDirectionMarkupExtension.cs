using System.ComponentModel;

namespace maui_localize_lm;

[ContentProperty(nameof(Path))]
public class FlowDirectionExtension : IMarkupExtension<BindingBase>, INotifyPropertyChanged
{
    public FlowDirection FlowDirection
        => LocalizationManager.FlowDirection;

    public string Path { get; set; } = ".";
    public BindingMode Mode { get; set; } = BindingMode.OneWay;
    public IValueConverter Converter { get; set; } = null;
    public string ConverterParameter { get; set; } = null;
    public string StringFormat { get; set; } = null;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding(nameof(FlowDirection), Mode, Converter, ConverterParameter, StringFormat, this);

    public FlowDirectionExtension()
        => LocalizationManager.FlowDirectionChanged += OnFlowDirectionChanged;
    ~FlowDirectionExtension()
        => LocalizationManager.FlowDirectionChanged -= OnFlowDirectionChanged;

    private void OnFlowDirectionChanged(object sender, EventArgs e)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlowDirection)));

    public event PropertyChangedEventHandler PropertyChanged;
}