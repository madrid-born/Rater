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
        private int _topicId;
        public MakeNewItemPage(DatabaseContext databaseContext, int topicId)
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
                
                // var item = new Item(name, _topic);
                var item = new Item { Name = name, ParentId = _topicId};
                // Functions.AddItem(item);
                _databaseContext.AddItem(item);
                MessagingCenter.Send(this, "UpdateTopicItemsPage", item);
                await Navigation.PopAsync();
            };
            sl.Children.Add(submitButton);
        
            Content = new ScrollView { Content = sl};
        }
    }