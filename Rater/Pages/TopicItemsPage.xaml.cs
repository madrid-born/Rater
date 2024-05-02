using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class TopicItemsPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private int _topicId;

        public TopicItemsPage(DatabaseContext dbContext, int topicId)
        {
            InitializeComponent();
            _databaseContext = dbContext;
            _topicId = topicId;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // MessagingCenter.Subscribe<MakeNewItemPage, Item>(this, "UpdateTopicItemsPage", (sender, item) => { _topic.Items.Add(item);});
            FillTheFront();
        }
        
        private void FillTheFront()
        {
            var itemStackLayout = new StackLayout
            {
                Spacing = 5
            };
        
            foreach (var item in _databaseContext.GetItems())
            {
                itemStackLayout.Add(ItemButton(item));
            }
            
            var newItemButton = new Button { Text = "Create New Item", BackgroundColor = Colors.Blue};
            newItemButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MakeNewItemPage(_databaseContext, _topicId));
            };
        
            Content = new ScrollView { Content = new StackLayout {Margin = 20, Children = {newItemButton, itemStackLayout}}};
        }
        
        private Button ItemButton(Item item)
        {
            var button = new Button
            {
                Text = item.Name + "\t" +item.MeanValue,
                BackgroundColor = Colors.Green
            };
            
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new ItemPage(_databaseContext, item.Id));
            };
            
            return button;
        }
    }