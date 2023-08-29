using maui_localize_lm.Resources.Strings;

namespace maui_localize_lm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LocalizationManager.Current.SetDefaultLocalizer<AppStrings>();

            MainPage = new AppShell();
        }
    }
}