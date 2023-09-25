using maui_localize_weird.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using Toolkit.Maui.Localization;

namespace maui_localize_weird
{
    //private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>(
    //    new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-SA" }
    //    .Select(s => new LanguageInfo(s)));

    public partial class MainPage : ContentPage
    {
        public LocalizationManager LM { get; }

        private IStringLocalizer Localizer { get; }
        public IList<CultureInfo> Cultures { get; }
        public ICommand SetCultureCommand { get; }

        int count = 0;
        public int Count => count;

        public string this[string key] => Localizer[key];

        public MainPage(LocalizationManager LM)
        {
            this.LM = LM;
            Localizer = ServiceHelper.GetService<IStringLocalizer<AppStrings>>();
            Cultures = new List<CultureInfo>(new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-SA" }.Select(s => new CultureInfo(s)));
            SetCultureCommand = new Command<CultureInfo>(SetCulture);

            InitializeComponent();
            BindingContext = this;
        }

        public void SetCulture(CultureInfo culture)
        {
            LM.Culture = culture;
            OnPropertyChanged("Item");
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            OnPropertyChanged(nameof(Count));

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }
}