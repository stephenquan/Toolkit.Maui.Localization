using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Toolkit.Maui.Localization;

public class LocalizationManager : INotifyPropertyChanged
{
    internal static Type DefaultStringResource;

    public static IStringLocalizer GetLocalizer<TStringResource>()
        => ServiceHelper.GetService<IStringLocalizer<TStringResource>>();
    public static IStringLocalizer GetLocalizer(Type StringResource = null)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { StringResource ?? DefaultStringResource }));

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (CultureInfo.CurrentUICulture.Name == value.Name
                && CultureInfo.CurrentCulture.Name == value.Name)
            {
                return;
            }
            value.ClearCachedData();
            CultureInfo.CurrentUICulture = value;
            CultureInfo.CurrentCulture = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Culture)));
            CultureChanged?.Invoke(this, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRightToLeft)));
        }
    }

    public bool IsRightToLeft
         => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

    public ICommand SetCultureCommand { get; }
    public void SetCulture(CultureInfo value)
    {
        Culture = value;
    }

    public LocalizationManager()
    {
        SetCultureCommand = new Command<CultureInfo>(SetCulture);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler<CultureInfo> CultureChanged;
}
