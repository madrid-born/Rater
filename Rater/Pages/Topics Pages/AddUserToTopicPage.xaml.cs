using Rater.Methods;

namespace Rater.Pages.Topics_Pages ;

    public partial class AddUserToTopicPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private readonly int _topicId;

        public AddUserToTopicPage(DatabaseContext databaseContext, int topicId)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
            _topicId = topicId;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }
        
        private void FillTheFront()
        {
            var sl = new StackLayout
            {
                Spacing = 10,
                Margin = 20
            };
            
            var nameEntry = new Entry { Placeholder = "UserName"};
            sl.Children.Add(nameEntry);
        
            var submitButton = new Button { Text = "Submit"};
            submitButton.Clicked += async (sender, e) =>
            {
                var name = nameEntry.Text;
                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Error", "Username can't be empty", "OK");
                    return;
                }

                if (_databaseContext.CheckUsernameInDatabase(name))
                {
                    await DisplayAlert("Error", "Username Doesnt Exist", "OK");
                    return;
                }

                var user = _databaseContext.GetUserByName(name);
                user.InviteToTopic(_topicId);
                _databaseContext.UpdateUser(user);
                await DisplayAlert("Error", "User Invited Successfully", "OK");
                await Navigation.PopAsync();
            };
            sl.Children.Add(submitButton);
        
            Content = new ScrollView { Content = sl};
        }
    }