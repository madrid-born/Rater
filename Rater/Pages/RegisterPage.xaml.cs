using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;

        public RegisterPage(DatabaseContext databaseContext)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage(_databaseContext);
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var username = UsernameEntry.Text;
                var password1 = PasswordEntry1.Text;
                var password2 = PasswordEntry2.Text;
            
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
            
                if (string.IsNullOrEmpty(password2))
                {
                    await DisplayAlert("Error", "Please fill both the Password fields", "OK");
                    return;
                }
            
                if (password1 != password2)
                {
                    await DisplayAlert("Error", "Passwords doesn't match", "OK");
                    return;
                }
            
                if (!_databaseContext.CheckUsernameInDatabase(username))
                {
                    await DisplayAlert("Error", "This Username had already been taken", "OK");
                    return;
                }
                
                var user = new User{Name = username, Password = password1, TopicsIdIncludedJson = Functions.SerializeIntList(new List<int>()), InvitedTopicsIdJson = Functions.SerializeIntList(new List<int>())};
                _databaseContext.AddUser(user);
                Functions.AuthorizeUser(user);
                await DisplayAlert("s","Registered Successfully", "s");
                
                Application.Current.MainPage = new NavigationPage(new MainPage(_databaseContext));
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error", exception.Message, "Done");
            }
        }
    }