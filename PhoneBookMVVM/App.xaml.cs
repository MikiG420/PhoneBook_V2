using PhoneBookMVVM.MVVM.Pages;

namespace PhoneBookMVVM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home());
        }
    }
}
