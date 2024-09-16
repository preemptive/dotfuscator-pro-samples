namespace MauiSample_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var NavPage = new NavigationPage(new MainPage());
            NavPage.BarBackground = Colors.Green;
            NavPage.BarTextColor = Colors.White;
            MainPage = NavPage;
        }
    }
}
