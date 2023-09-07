using System.ComponentModel;
using System.Globalization;

namespace maui_localize_di;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    public LanguageInfo(string language) : base(language)
        => LocalizationManager.CurrentCultureChanged += OnCultureChanged;
    ~LanguageInfo()
        => LocalizationManager.CurrentCultureChanged -= OnCultureChanged;
    private void OnCultureChanged(object sender, CultureInfo culture)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));
    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;
    public event PropertyChangedEventHandler PropertyChanged;
}

