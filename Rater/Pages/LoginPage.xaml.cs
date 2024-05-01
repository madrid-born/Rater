using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;

namespace Rater.Pages ;

    public partial class LoginPage : ContentPage
    {
        private DatabaseContext _databaseContext;
        public LoginPage(DatabaseContext databaseContext)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password1 = PasswordEntry.Text;
            
            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "Username field can't be empty", "OK");
                return;
            }
            
            if (string.IsNullOrEmpty(password1))
            {
                await DisplayAlert("Error", "Password field can't be empty", "OK");
                return;
            }

            // var person = new Person(username, password1);
            //
            // if (!Functions.CheckUSerInDatabase(person))
            // {
            //     await DisplayAlert("Error", "This Username had already been taken", "OK");
            //     return;
            // }
            //
            // Functions.AuthorizeUser(person);
            
            Application.Current.MainPage = new NavigationPage(new TopicsPage());
        }
    }