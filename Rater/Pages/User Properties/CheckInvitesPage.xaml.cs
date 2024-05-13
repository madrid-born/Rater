using Rater.Methods;

namespace Rater.Pages.User_Properties ;

    public partial class CheckInvitesPage : ContentPage
    {
        private DatabaseContext _databaseContext;
        public CheckInvitesPage(DatabaseContext databaseContext)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
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
                Spacing = 20, Margin = 20
            };

            foreach (var topicId in _databaseContext.GetUserByName(Functions.GetUsername()).TopicsInvited())
            {
                sl.Children.Add(CreateVsl(topicId));
            }
            
            Content = new ScrollView { Content = sl};

        }

        private Frame CreateVsl(int topicId)
        {
            var topic = _databaseContext.GetTopicById(topicId);
            var user = _databaseContext.GetUserByName(Functions.GetUsername());
            var nameLabel = new Label { Text = $"Name : {topic.Name}" ,TextColor= Colors.DarkSlateGray,  FontSize=20};
            var ownersNameLabel = new Label { Text = $"Owner : {topic.OwnersName}" ,TextColor= Colors.DimGray,  FontSize=20};

            var acceptButton = new Button { Text = "Accept", BackgroundColor = Colors.Green, TextColor= Colors.White, FontSize=25,  HorizontalOptions= LayoutOptions.Fill};
            acceptButton.Clicked += async (sender, args) =>
            {
                user.AcceptInvite(topicId);
                topic.AddMember(_databaseContext, user.Name);
                _databaseContext.UpdateUser(user);
                _databaseContext.UpdateTopic(topic);
                await DisplayAlert("Message", "Accepted Successfully", "OK");
            };
            var declineButton = new Button { Text = "Decline", BackgroundColor = Colors.Red, TextColor= Colors.White, FontSize=25,  HorizontalOptions= LayoutOptions.Fill};
            declineButton.Clicked += async (sender, args) =>
            {
                user.DeclineInvite(topicId);
                _databaseContext.UpdateUser(user);
                await DisplayAlert("Message", "Declined Successfully", "OK");
            };
            var hsl = new StackLayout
            {
                Spacing = 10,
                Orientation= StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { acceptButton, declineButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.BlanchedAlmond,
                Content = new VerticalStackLayout {Children = { nameLabel, ownersNameLabel, hsl }}
            };
            
            return frame;
        }
    }