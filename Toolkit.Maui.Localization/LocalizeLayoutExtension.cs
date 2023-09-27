using System.Globalization;

namespace Toolkit.Maui.Localization;

public enum LayoutMode
{
    IsRightToLeft,
    FlowDirection,
    RotationY
}

[ContentProperty(nameof(Mode))]
public class LocalizeLayoutExtension : IMarkupExtension<BindingBase>, IValueConverter
{
    private LocalizationManager lm;
    public LocalizationManager LM
        => lm ??= ServiceHelper.GetService<LocalizationManager>();

    public LayoutMode Mode { get; set; } = LayoutMode.IsRightToLeft;

    public object ProvideValue(IServiceProvider serviceProvider)
        => (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        => new Binding(nameof(LocalizationManager.IsRightToLeft), BindingMode.OneWay, this, null, null, LM);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        return Mode switch
        {
            LayoutMode.IsRightToLeft => (bool) value,
            LayoutMode.FlowDirection => (bool) value ? FlowDirection.RightToLeft : FlowDirection.LeftToRight,
            LayoutMode.RotationY => (bool) value ? 180 : 0,
            _ => value
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}