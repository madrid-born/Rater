using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

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
                Spacing = 20, Margin = 10
            };

            foreach (var topicId in _databaseContext.GetUserByName(Functions.GetUsername()).TopicsInvited())
            {
                sl.Children.Add(CreateVsl(topicId));
            }
            
            Content = new ScrollView { Content = sl};

        }

        private VerticalStackLayout CreateVsl(int topicId)
        {
            var topic = _databaseContext.GetTopicById(topicId);
            var user = _databaseContext.GetUserByName(Functions.GetUsername());
            var nameLabel = new Label { Text = $"Name : {topic.Name}" };
            var ownersNameLabel = new Label { Text = $"Owner : {topic.OwnersName}" };

            var acceptButton = new Button { Text = "Accept", BackgroundColor = Colors.Green};
            acceptButton.Clicked += async (sender, args) =>
            {
                user.AcceptInvite(topicId);
                topic.AddMember(_databaseContext, user.Name);
                _databaseContext.UpdateUser(user);
                _databaseContext.UpdateTopic(topic);
                await DisplayAlert("Message", "Accepted Successfully", "OK");
            };
            var declineButton = new Button { Text = "Decline", BackgroundColor = Colors.Red};
            declineButton.Clicked += async (sender, args) =>
            {
                user.DeclineInvite(topicId);
                _databaseContext.UpdateUser(user);
                await DisplayAlert("Message", "Declined Successfully", "OK");
            };
            var hsl = new HorizontalStackLayout
            {
                Spacing = 10,
                Children = { acceptButton, declineButton }
            };
            
            var vsl = new VerticalStackLayout
            {
                BackgroundColor = Colors.Wheat,
                Children = { nameLabel, ownersNameLabel, hsl }
            };
            
            return vsl;
        }
    }