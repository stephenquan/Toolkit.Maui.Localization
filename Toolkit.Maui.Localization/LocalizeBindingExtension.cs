using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Maui.Localization;

[ContentProperty(nameof(Path))]
public class LocalizeBindingExtension : IMarkupExtension<BindingBase>, IMultiValueConverter
{
    private LocalizationManager lm;
    public LocalizationManager LM
        => lm ??= ServiceHelper.GetService<LocalizationManager>();

    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= LocalizationManager.GetLocalizer(StringResource);

    /// <inheritdoc/>
    public string Path { get; set; } = ".";

    /// <inheritdoc/>
    public BindingMode Mode { get; set; } = BindingMode.OneWay;

    /// <inheritdoc/>
    public string StringFormat { get; set; } = "{0}";

    /// <inheritdoc/>
    public IValueConverter Converter { get; set; } = null;

    /// <inheritdoc/>
    public object ConverterParameter { get; set; } = null;

    /// <inheritdoc/>
    public object Source { get; set; } = null;

    public Type StringResource { get; set; } = null;

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
            Converter = this,
            Mode = Mode,
            Bindings = new Collection<BindingBase>
            {
                new Binding(Path, Mode, Converter, ConverterParameter, null, Source),
                new Binding(nameof(LocalizationManager.Culture), BindingMode.OneWay, null, null, null, LM)
            }
        };
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 0 || values[0] == null)
        {
            return "";
        }

        if (!string.IsNullOrEmpty(LocalizeZero) && IsZero(values[0]))
        {
            return Localizer.GetString(LocalizeZero, values);
        }

        if (!string.IsNullOrEmpty(LocalizeOne) && IsOne(values[0]))
        {
            return Localizer.GetString(LocalizeOne, values);
        }

        if (!string.IsNullOrEmpty(LocalizeFormat))
        {
            return Localizer.GetString(LocalizeFormat, values);
        }

        if (LocalizeValue)
        {
            return Localizer.GetString(values[0].ToString());
        }

        return values[0];
    }

    static bool IsZero(object value)
    {
        if (value == null)
        {
            return false;
        }
        if (value.GetType() == typeof(int) && (int)value == 0)
        {
            return true;
        }
        return false;
    }

    static bool IsOne(object value)
    {
        if (value == null)
        {
            return false;
        }
        if (value.GetType() == typeof(int) && (int)value == 1)
        {
            return true;
        }
        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
