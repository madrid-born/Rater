using Rater.Methods;
using Rater.Models;
using Rater.Pages.Items_pages;

namespace Rater.Pages.Topics_Pages ;

    public partial class TopicPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private int _topicId;
        private List<Item> _itemsList;

        public TopicPage(DatabaseContext dbContext, int topicId)
        {
            InitializeComponent();
            _databaseContext = dbContext;
            _topicId = topicId;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _itemsList = _databaseContext.GetItemsForTopic(_topicId);
            FillTheFront();
        }
        
        private void FillTheFront()
        {
            var propertiesStackLayout = new StackLayout
            {
                Spacing = 20, Margin = 20
            };
            
            var newItemButton = new Button { 
                Text = "Create New Item",
                BackgroundColor = Colors.Yellow ,
                MinimumHeightRequest = 100 ,
                TextColor = Colors.SaddleBrown ,
                FontSize = 30 ,
                CornerRadius = 25 };
            
            newItemButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MakeNewItemPage(_databaseContext, _topicId));
            };
            propertiesStackLayout.Children.Add(newItemButton);
            
            var addUserButton = new Button
            {
                Text = "Add new User",
                BackgroundColor = Colors.Orange ,
                MinimumHeightRequest = 100 ,
                TextColor = Colors.SaddleBrown ,
                FontSize = 30 ,
                CornerRadius = 25             
            };
            addUserButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new AddUserToTopicPage(_databaseContext, _topicId));
            };
            propertiesStackLayout.Children.Add(addUserButton);
            
            var itemStackLayout = new StackLayout
            {
                Spacing = 10, Margin = 20
            };
        
            foreach (var item in _itemsList)
            {
                itemStackLayout.Add(ItemButton(item));
            }

            Content = new ScrollView { Content = new StackLayout { Children = {propertiesStackLayout, itemStackLayout}}};
        }
        
        private Button ItemButton(Item item)
        {
            var button = new Button
            {
                Text = item.Name + "\t" +item.MeanValue,
                BackgroundColor = Colors.GreenYellow ,
                MinimumHeightRequest = 50 ,
                TextColor = Colors.SaddleBrown ,
                FontSize = 30 ,
                CornerRadius = 25 
            };
            
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new ItemPage(_databaseContext, item.Id));
            };
            
            return button;
        }
    }