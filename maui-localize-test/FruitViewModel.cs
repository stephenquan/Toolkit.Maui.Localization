using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Globalization;

namespace maui_localize_test;

public partial class FruitViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string _name;

    public string DisplayName => Resources.Strings.AppStrings.ResourceManager.GetString(Name);

    [ObservableProperty]
    private bool _isChecked;

    public FruitViewModel()
    {
        WeakReferenceMessenger.Default.Register<CultureInfo>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(DisplayName));
        });
    }

    ~FruitViewModel()
    {
        WeakReferenceMessenger.Default.Unregister<CultureInfo>(this);
    }
}
