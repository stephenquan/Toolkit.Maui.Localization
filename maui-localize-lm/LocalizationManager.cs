using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public partial class LocalizationManager : ObservableObject
{
    private static LocalizationManager _current = new LocalizationManager();
    public static LocalizationManager Current => _current;

    public event EventHandler CultureChanged;

    public FlowDirection FlowDirection => Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

    private IStringLocalizer _localizer;

    public void SetLocalizer<T>()
    {
        _localizer = ServiceHelper.GetService<IStringLocalizer<T>>();
    }

    private IDictionary<string, IStringLocalizer> _extraLocalizers = new Dictionary<string, IStringLocalizer>();

    public void SetExtraLocalizer<T>(string name)
    {
        IStringLocalizer localizer = ServiceHelper.GetService<IStringLocalizer<T>>();
        if (_extraLocalizers.ContainsKey(name))
        {
            _extraLocalizers[name] = localizer;
        }
        else
        {
            _extraLocalizers.Add(name, localizer);
        }
    }

    public string this[string name]
    {
        get
        {
            string[] names = name.Split(';');
            IStringLocalizer localizer = _localizer;
            string _name = name;
            if (names.Length >= 2)
            {
                localizer = _extraLocalizers[names[0]];
                _name = names[1];
            }
            return localizer == null ? name : localizer?[_name];
        }
    }

    public string this[string name, params object[] arguments]
    {
        get
        {
            string[] names = name.Split(';');
            IStringLocalizer localizer = _localizer;
            string _name = name;
            if (names.Length >= 2)
            {
                localizer = _extraLocalizers[names[0]];
                _name = names[1];
            }
            return localizer == null ? name : localizer?[_name, arguments];
        }
    }

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null)
            {
                return;
            }
            CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged("Item");
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
