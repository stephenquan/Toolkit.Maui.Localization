using System.Globalization;

namespace Toolkit.Maui.Localization;

internal class StringFormatConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0)
        {
            return "";
        }

        if (values[values.Length - 1] == null)
        {
            return "";
        }

        return String.Format(values[values.Length - 1].ToString(), values);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
