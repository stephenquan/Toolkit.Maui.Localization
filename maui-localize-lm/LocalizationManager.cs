using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public partial class LocalizationManager : ObservableObject
{
    private static LocalizationManager _current;
    public static LocalizationManager Current => _current ??= new LocalizationManager();
    public event EventHandler CultureChanged;
    public FlowDirection FlowDirection => Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer => _localizer;
    public IStringLocalizer SetDefaultLocalizer<T>() => _localizer = ServiceHelper.GetService<IStringLocalizer<T>>();
    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null) return;
            if (value.Name == CultureInfo.CurrentUICulture.Name) return;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged(nameof(Localizer));
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
