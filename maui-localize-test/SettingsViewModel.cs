using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_test;

public partial class SettingsViewModel : ObservableObject
{
    public string TITLE_SELECT_LANGUAGE => Resources.Strings.AppStrings.TITLE_SELECT_LANGUAGE;

    [ObservableProperty]
    private ObservableCollection<LanguageViewModel> _languages;

    private LanguageViewModel _selectedLanguage;
    public LanguageViewModel SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (_selectedLanguage != value)
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
                if (value != null && value.Name != CultureInfo.CurrentCulture.Name)
                {
                    CultureInfo.CurrentCulture = value.Culture;
                    CultureInfo.CurrentUICulture = value.Culture;
                    WeakReferenceMessenger.Default.Send(CultureInfo.CurrentCulture);
                }
            }
        }
    }

    public SettingsViewModel()
    {
        Languages = new ObservableCollection<LanguageViewModel>()
        {
            new LanguageViewModel(){ Name = "en-US" },
            new LanguageViewModel(){ Name = "fr-FR" },
            new LanguageViewModel(){ Name = "de-DE" },
            new LanguageViewModel(){ Name = "zh-CN" }
        };
        SelectedLanguage = Languages.FirstOrDefault(l => l.Name == CultureInfo.CurrentCulture.Name);

        WeakReferenceMessenger.Default.Register<CultureInfo>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(TITLE_SELECT_LANGUAGE));
        });
    }

    ~SettingsViewModel()
    {
        WeakReferenceMessenger.Default.Unregister<CultureInfo>(this);   
    }
}
