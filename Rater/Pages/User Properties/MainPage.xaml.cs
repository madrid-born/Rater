using Rater.Methods;
using Rater.Models;
using Rater.Pages.Topics_Pages;

namespace Rater.Pages.User_Properties ;

    public partial class MainPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private List<Topic> _topicsList ;
        
        public MainPage(DatabaseContext dbContext)
        {
            InitializeComponent();
            _databaseContext = dbContext;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _topicsList = _databaseContext.GetTopicsForUser();
            FillTheFront();
        }
        
        private void FillTheFront()
        {
            var sl = new StackLayout
            {
                Spacing = 20, Margin = 20
            };
            
            var checkInvitesButton = new Button { Text = "Check your invites", BackgroundColor = Colors.Orange ,MinimumHeightRequest = 100 ,TextColor = Colors.SaddleBrown ,FontSize = 30 ,CornerRadius = 25 };
            checkInvitesButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new CheckInvitesPage(_databaseContext));
            };
            sl.Children.Add(checkInvitesButton);

            var newTopicButton = new Button { Text = "Create New Topic" ,BackgroundColor = Colors.GreenYellow ,MinimumHeightRequest = 100 ,TextColor = Colors.SaddleBrown ,FontSize = 30 ,CornerRadius = 25 };
            newTopicButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MakeNewTopicPage(_databaseContext));
            };
            sl.Children.Add(newTopicButton);
            
            var topicsListButton = new Button { Text = "Your Topics" ,BackgroundColor = Colors.Yellow ,MinimumHeightRequest = 100 ,TextColor = Colors.SaddleBrown ,FontSize = 30 ,CornerRadius = 25 };
            topicsListButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new UserTopics(_databaseContext));
            };
            sl.Children.Add(topicsListButton);

            Content = new ScrollView { Content = sl };
        }
    }