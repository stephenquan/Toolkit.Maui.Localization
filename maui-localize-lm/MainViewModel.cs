﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_localize_lm.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_lm;

public partial class MainViewModel : ObservableObject
{
    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer
        => _localizer ??= LocalizationManager.GetStringLocalizer<AppStrings>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;

    public string ClickText => Counter switch
    {
        0 => Localizer["STR_CLICK_ME"],
        1 => Localizer["STR_CLICKED_1_TIME"],
        _ => Localizer["STR_CLICKED_N_TIMES", Counter]
    };

    [RelayCommand]
    private void ClickMe()
        => Counter++;

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>()
    {
        new LanguageInfo("en-US"),
        new LanguageInfo("fr-FR"),
        new LanguageInfo("de-DE"),
        new LanguageInfo("zh-CN"),
        new LanguageInfo("ar-SA"),
    };

    [RelayCommand]
    private void ChangeLanguage(CultureInfo language)
        => LocalizationManager.Current.CurrentCulture = language;

    public MainViewModel()
        => LocalizationManager.CurrentCultureChanged += OnCurrentCultureChanged;

    ~MainViewModel()
        => LocalizationManager.CurrentCultureChanged -= OnCurrentCultureChanged;

    private void OnCurrentCultureChanged(object sender, CultureInfo culture)
        => OnPropertyChanged(nameof(ClickText));
}
