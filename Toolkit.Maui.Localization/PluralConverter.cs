using System.Globalization;

namespace Toolkit.Maui.Localization;

internal class PluralConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0)
        {
            return "";
        }

        if (values.Length == 1)
        {
            return "";
        }

        if (values.Length > 2 && IsOne(values[0]) && values[2] != null)
        {
            return String.Format(values[2].ToString(), values);
        }

        if (values.Length > 3 && IsZero(values[0]) && values[3] != null)
        {
            return String.Format(values[3].ToString(), values);
        }

        if (values[1] == null)
        {
            return "";
        }

        return String.Format(values[1].ToString(), values);
    }

    private static bool IsOne(object value)
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

    private static bool IsZero(object value)
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

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
