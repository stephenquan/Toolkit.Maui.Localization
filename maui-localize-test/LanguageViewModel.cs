using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Globalization;

namespace maui_localize_test;

public partial class LanguageViewModel : ObservableObject
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;

    public string Name
    {
        get => _culture.Name;
        set
        {
            if (value != _culture.Name)
            {
                _culture = new CultureInfo(value);
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
                OnPropertyChanged(nameof(NativeName));
                OnPropertyChanged(nameof(EnglishName));
                OnPropertyChanged(nameof(TwoLetterISOLanguageName));
                OnPropertyChanged(nameof(Culture));
                OnPropertyChanged(nameof(IsChecked));
            }
        }
    }

    public string DisplayName => _culture.DisplayName;
    public string NativeName => _culture.NativeName;
    public string EnglishName => _culture.EnglishName;
    public string TwoLetterISOLanguageName => _culture.TwoLetterISOLanguageName;

    public bool IsChecked => _culture.Name == CultureInfo.CurrentCulture.Name;
    public CultureInfo Culture => _culture;

    public override string ToString()
    {
        return NativeName;
    }

    public LanguageViewModel()
    {
        WeakReferenceMessenger.Default.Register<CultureInfo>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(IsChecked));
        });
    }

    ~LanguageViewModel()
    {
        WeakReferenceMessenger.Default.Unregister<CultureInfo>(this);
    }
}
