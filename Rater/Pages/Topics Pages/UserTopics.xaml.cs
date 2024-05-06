using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages.Topics_Pages ;

    public partial class UserTopics : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private List<Topic> _topicsList ;

        public UserTopics(DatabaseContext dbContext)
        {
            InitializeComponent();
            _topicsList = _databaseContext.GetTopicsForUser();
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
                Spacing = 5
            };
        
            foreach (var topic in _topicsList)
            {
                sl.Add(TopicButton(topic));
            }
            
            
            
            Content = new ScrollView { Content = sl};
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