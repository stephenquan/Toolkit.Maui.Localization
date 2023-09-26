using Toolkit.Maui.Localization.Sample.Resources.Strings;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using Toolkit.Maui.Localization;

namespace Toolkit.Maui.Localization.Sample
{
    public partial class MainPage : ContentPage
    {
        public LocalizationManager LM { get; }

        public IList<CultureInfo> Cultures { get; }

        public static readonly BindableProperty CountProperty
            = BindableProperty.Create(nameof(Count), typeof(int), typeof(MainPage), 0);

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public MainPage(LocalizationManager LM)
        {
            this.LM = LM;
            Cultures = new List<CultureInfo>(new string[] { "en-US", "fr-FR", "de-DE", "zh-CN", "ar-AE" }.Select(s => new CultureInfo(s)));

            InitializeComponent();
            BindingContext = this;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Count++;
        }

    }
}