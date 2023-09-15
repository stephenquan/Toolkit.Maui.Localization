using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_localize_lm.Localization;

/// <summary>
/// BindingExtension
/// </summary>
[ContentProperty(nameof(Path))]
public class LocalizeBindingExtension : IMarkupExtension<BindingBase>
{
    /// <inheritdoc/>
    public string Path { get; set; } = ".";

    /// <inheritdoc/>
    public BindingMode Mode { get; set; } = BindingMode.OneWay;

    /// <inheritdoc/>
    public string StringFormat { get; set; } = "{0}";

    public IValueConverter Converter { get; set; } = null;

    /// <inheritdoc/>
    public object ConverterParameter { get; set; } = null;

    /// <inheritdoc/>
    public object Source { get; set; } = null;

    /// <inheritdoc/>
    public Type StringResource { get; set; }

    /// <inheritdoc/>
    public bool LocalizeValue { get; set; } = false;

    /// <inheritdoc/>
    public string LocalizeFormat { get; set; }

    /// <inheritdoc/>
    public string LocalizeOne { get; set; }

    /// <inheritdoc/>
    public string LocalizeZero { get; set; }

    /// <inheritdoc/>
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    }

    BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
    {
        return new MultiBinding()
        {
            StringFormat = StringFormat,
            Converter = new LocalizeBindingConverter()
            {
                StringResource = StringResource,
                LocalizeValue = LocalizeValue,
                LocalizeFormat = LocalizeFormat,
                LocalizeOne = LocalizeOne,
                LocalizeZero = LocalizeZero
            },
            Mode = Mode,
            Bindings = new Collection<BindingBase>
            {
                new Binding(Path, Mode, Converter, ConverterParameter, null, null),
                new Binding(nameof(LocalizationManager.CurrentCulture), BindingMode.OneWay, null, null, null, LocalizationManager.Current)
            }
        };
    }
}