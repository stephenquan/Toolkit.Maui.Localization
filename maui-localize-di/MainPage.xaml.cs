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