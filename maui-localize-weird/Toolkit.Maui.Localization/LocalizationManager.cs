using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace Toolkit.Maui.Localization;

public class LocalizationManager : INotifyPropertyChanged
{
    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            Debug.WriteLine($"Culture {value.Name}");
            if (CultureInfo.CurrentUICulture.Name == value.Name
                && CultureInfo.CurrentCulture.Name == value.Name)
            {
                return;
            }
            value.ClearCachedData();
            CultureInfo.CurrentUICulture = value;
            CultureInfo.CurrentCulture = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Culture)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
