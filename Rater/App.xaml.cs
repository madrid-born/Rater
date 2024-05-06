using Rater.Pages;
using Rater.Pages.Login_and_Register;

namespace Rater ;

    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();

            MainPage = loginPage;
        }
    }