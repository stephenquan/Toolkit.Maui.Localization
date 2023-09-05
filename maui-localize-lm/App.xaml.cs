using maui_localize_lm.Resources.Strings;

namespace maui_localize_lm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LocalizationManager.DefaultResourceType = typeof(AppStrings);
            MainPage = new AppShell();
        }
    }
}