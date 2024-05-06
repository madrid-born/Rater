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
            var propertiesStackLayout = new StackLayout
            {
                Spacing = 5
            };
            
            var checkInvitesButton = new Button { Text = "Check your invites" ,BackgroundColor = Colors.Aqua};
            checkInvitesButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new CheckInvitesPage(_databaseContext));
            };
            propertiesStackLayout.Children.Add(checkInvitesButton);

            var newTopicButton = new Button { Text = "Create New Topic" ,BackgroundColor = Colors.Aqua};
            newTopicButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MakeNewTopicPage(_databaseContext));
            };
            propertiesStackLayout.Children.Add(newTopicButton);
            
            var topicStackLayout = new StackLayout
            {
                Spacing = 5
            };
        
            foreach (var topic in _topicsList)
            {
                topicStackLayout.Add(TopicButton(topic));
            }
            
            
            
            Content = new ScrollView { Content = new StackLayout {Margin = 20, Children = {propertiesStackLayout, topicStackLayout}}};
        }
        
        private Button TopicButton(Topic topic)
        {
            var button = new Button
            {
                Text = topic.Name,
                BackgroundColor = Colors.Red
            };
            
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new TopicPage(_databaseContext, topic.Id));
            };
            
            return button;
        }
    }