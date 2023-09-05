using System.ComponentModel;
using System.Globalization;

namespace maui_localize_lm;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    public LanguageInfo(string language) : base(language)
        => LocalizationManager.CultureChanged += OnCultureChanged;

    ~LanguageInfo()
        => LocalizationManager.CultureChanged -= OnCultureChanged;

    private void OnCultureChanged(object sender, EventArgs e)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));

    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;

    public event PropertyChangedEventHandler PropertyChanged;
}
