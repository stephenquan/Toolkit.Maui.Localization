using maui_localize_resx.Resources.Strings;
using System.Globalization;

namespace maui_localize_resx;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        CounterBtn.Text = GetClickText();
        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnLanguageClicked(object sender, EventArgs e)
    {
        string localeName = ((ImageButton)sender).CommandParameter.ToString();
        CultureInfo.CurrentUICulture = new CultureInfo(localeName);
        Title = AppStrings.TITLE_MAIN;
        HelloLbl.Text = AppStrings.LBL_HELLO;
        WelcomeLbl.Text = AppStrings.LBL_WELCOME;
        CounterBtn.Text = GetClickText();
    }

    private string GetClickText() => count switch
    {
        0 => AppStrings.STR_CLICK_ME,
        1 => AppStrings.STR_CLICKED_1_TIME,
        _ => string.Format(AppStrings.STR_CLICKED_N_TIMES, count)
    };
}