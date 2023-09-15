using maui_localize_resx.Resources.Strings;
using System.Globalization;

namespace maui_localize_resx;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        CultureLbl.Text = CultureInfo.CurrentUICulture.NativeName;
        LangENBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "en-US" ? Colors.Orange : Colors.Transparent;
        LangDEBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "de-DE" ? Colors.Orange : Colors.Transparent;
        LangFRBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "fr-FR" ? Colors.Orange : Colors.Transparent;
        LangZHBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "zh-CN" ? Colors.Orange : Colors.Transparent;
        LangARBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "ar-SA" ? Colors.Orange : Colors.Transparent;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        CounterBtn.Text = GetClickText();
        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnLanguageClicked(object sender, EventArgs e)
    {
        string LocaleName = ((ImageButton)sender).CommandParameter.ToString();
        CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture = new CultureInfo(LocaleName);
        FlowDirection = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft
            ? FlowDirection.RightToLeft
            : FlowDirection.LeftToRight;
        Title = AppStrings.TITLE_MAIN;
        HelloLbl.Text = AppStrings.LBL_HELLO;
        WelcomeLbl.Text = AppStrings.LBL_WELCOME;
        CounterBtn.Text = GetClickText();
        CultureLbl.Text = CultureInfo.CurrentUICulture.NativeName;
        LangENBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "en-US" ? Colors.Orange : Colors.Transparent;
        LangDEBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "de-DE" ? Colors.Orange : Colors.Transparent;
        LangFRBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "fr-FR" ? Colors.Orange : Colors.Transparent;
        LangZHBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "zh-CN" ? Colors.Orange : Colors.Transparent;
        LangARBtn.BackgroundColor = CultureInfo.CurrentUICulture.Name == "ar-SA" ? Colors.Orange : Colors.Transparent;
    }

    private string GetClickText() => count switch
    {
        0 => AppStrings.STR_CLICK_ME,
        1 => AppStrings.STR_CLICKED_1_TIME,
        _ => string.Format(AppStrings.STR_CLICKED_N_TIMES, count)
    };
}