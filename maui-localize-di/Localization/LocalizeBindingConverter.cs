using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_localize_di.Localization;

internal class LocalizeBindingConverter : IMultiValueConverter
{
    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= LocalizationManager.GetLocalizer(StringResource);

    public Type StringResource { get; set; }
    public string LocalizeFormat { get; set; }
    public string LocalizeOne { get; set; }
    public string LocalizeZero { get; set; }
    public bool LocalizeValue { get; set; } = false;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 0 || values[0] == null)
        {
            return null;
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
        if (!value.GetType().IsValueType)
        {
            return false;
        }
        return (int)value == 0;
    }

    static bool IsOne(object value)
    {
        return (int)value == 1;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
