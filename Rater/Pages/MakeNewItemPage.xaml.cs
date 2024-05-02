using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class MakeNewItemPage : ContentPage
    {
        private DatabaseContext _databaseContext;
        private int _parentId;
        public MakeNewItemPage(DatabaseContext databaseContext, int parentId)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
            _parentId = parentId;
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
            
            var nameEntry = new Entry { Placeholder = "Name"};
            sl.Children.Add(nameEntry);
        
            var submitButton = new Button { Text = "Submit"};
            submitButton.Clicked += async (sender, e) =>
            {
                var name = nameEntry.Text;
                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Error", "Name can't be empty", "OK");
                    return;
                }
                
                var item = new Item { Name = name, ParentId = _parentId, DateCreated =  DateTime.Now};
                var topic = _databaseContext.GetTopicById(_parentId);
                item.DefaultValues(topic);
                _databaseContext.AddItem(item);
                var topicItems = Functions.DeserializeIntList(topic.ItemsIdJson);
                topicItems.Add(item.Id);
                topic.ItemsIdJson = Functions.SerializeIntList(topicItems); 
                _databaseContext.UpdateTopic(topic);
                // MessagingCenter.Send(this, "UpdateTopicItemsPage", item);
                await Navigation.PopAsync();
            };
            sl.Children.Add(submitButton);
        
            Content = new ScrollView { Content = sl};
        }
    }