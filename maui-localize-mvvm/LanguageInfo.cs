using System.ComponentModel;
using System.Globalization;

namespace maui_localize_mvvm;

public class LanguageInfo : CultureInfo, INotifyPropertyChanged
{
    public static EventHandler CultureChanged;

    public static CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value.Name == CultureInfo.CurrentUICulture.Name) return;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CultureChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public LanguageInfo(string language) : base(language)
        => CultureChanged += OnCultureChanged;

    ~LanguageInfo()
        => CultureChanged -= OnCultureChanged;

    private void OnCultureChanged(object sender, EventArgs e)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCurrent)));

    public bool IsCurrent
        => Name == CultureInfo.CurrentUICulture.Name;

    public event PropertyChangedEventHandler PropertyChanged;
}
