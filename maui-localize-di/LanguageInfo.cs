using System.ComponentModel;
using System.Globalization;

namespace maui_localize_di;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    private LocalizationManager LM;

    public LanguageInfo(LocalizationManager lm, string language) : base(language)
    {
        LM = lm;
        LM.CurrentCultureChanged += OnCultureChanged;
    }

    ~LanguageInfo()
    {
        LM.CurrentCultureChanged -= OnCultureChanged;
    }

    private void OnCultureChanged(object sender, CultureInfo culture)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));
    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;
    public event PropertyChangedEventHandler PropertyChanged;
}

