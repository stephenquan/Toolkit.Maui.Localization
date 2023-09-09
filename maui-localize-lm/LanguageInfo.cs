using System.ComponentModel;
using System.Globalization;

namespace maui_localize_lm;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    public LanguageInfo(string language) : base(language)
        => LocalizationManager.Current.CurrentCultureChanged += OnCurrentCultureChanged;
    ~LanguageInfo()
        => LocalizationManager.Current.CurrentCultureChanged -= OnCurrentCultureChanged;
    private void OnCurrentCultureChanged(object sender, CultureInfo culture)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));
    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;
    public event PropertyChangedEventHandler PropertyChanged;
}
