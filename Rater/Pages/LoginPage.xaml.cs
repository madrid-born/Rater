using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        public LoginPage(DatabaseContext databaseContext)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage(_databaseContext);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            
            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "Username field can't be empty", "OK");
                return;
            }
            
            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Password field can't be empty", "OK");
                return;
            }

            var user = new User{Name = username, Password = password};
            
            if (!_databaseContext.CheckUserAuthentication(user))
            {
                await DisplayAlert("Error", "Username or Password is incorrect", "OK");
                return;
            }
            
            Functions.AuthorizeUser(user);
            
            Application.Current.MainPage = new NavigationPage(new TopicsPage(_databaseContext));
            
            // TODO: activate this on release
            // Application.Current.MainPage = new NavigationPage(new HomeTabbedPage());
        }
    }