using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class TopicsPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private List<Topic> _topicsList ;
        
        public TopicsPage(DatabaseContext dbContext)
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
            var topicStackLayout = new StackLayout
            {
                Spacing = 5
            };
        
            foreach (var topic in _topicsList)
            {
                topicStackLayout.Add(TopicButton(topic));
            }
            
            var newTopicButton = new Button { Text = "Create New Topic" ,BackgroundColor = Colors.Aqua};
            newTopicButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MakeNewTopicPage(_databaseContext));
            };
            
            Content = new ScrollView { Content = new StackLayout {Margin = 20, Children = {newTopicButton, topicStackLayout}}};
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
                await Navigation.PushAsync(new TopicItemsPage(_databaseContext, topic.Id));
            };
            
            return button;
        }
    }