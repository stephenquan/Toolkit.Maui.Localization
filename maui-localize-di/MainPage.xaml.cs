using maui_localize_di.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_di
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

    }
}