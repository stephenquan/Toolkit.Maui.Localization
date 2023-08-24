using maui_localize_lm.Resources.Strings;

namespace maui_localize_lm
{
    public partial class App : Application
    {
        public App()
        {
            LocalizationManager.Current.SetLocalizer<AppStrings>();

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}