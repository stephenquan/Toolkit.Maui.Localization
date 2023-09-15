using System.ComponentModel;
using System.Globalization;

namespace maui_localize_di.Localization;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    private LocalizationManager _lm;
    private LocalizationManager LM
        => _lm ??= ServiceHelper.GetService<LocalizationManager>();

    public LanguageInfo(string language) : base(language)
        => LM.CurrentCultureChanged += OnCultureChanged;

    ~LanguageInfo()
        => LM.CurrentCultureChanged -= OnCultureChanged;

    private void OnCultureChanged(object sender, CultureInfo culture)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));

    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;

    public event PropertyChangedEventHandler PropertyChanged;
}

