using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Models;

namespace Rater.Pages ;

    public partial class TopicItemsPage : ContentPage
    {
        private Topic _topic;
        
        public TopicItemsPage(Topic topic)
        {
            InitializeComponent();
            _topic = topic;
        }
        
        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     MessagingCenter.Subscribe<MakeNewItemPage, Item>(this, "UpdateTopicItemsPage", (sender, item) => { _topic.Items.Add(item);});
        //     FillTheFront();
        // }
        //
        // private void FillTheFront()
        // {
        //     var itemStackLayout = new StackLayout
        //     {
        //         Spacing = 5
        //     };
        //
        //     foreach (var topic in _topic.Items)
        //     {
        //         itemStackLayout.Add(ItemButton(topic));
        //     }
        //     
        //     var newItemButton = new Button { Text = "Create New Item", BackgroundColor = Colors.Blue};
        //     newItemButton.Clicked += async (sender, e) =>
        //     {
        //         await Navigation.PushAsync(new MakeNewItemPage(_topic));
        //     };
        //
        //     Content = new ScrollView { Content = new StackLayout {Margin = 20, Children = {newItemButton, itemStackLayout}}};
        // }
        //
        // private Button ItemButton(Item item)
        // {
        //     var button = new Button
        //     {
        //         Text = item.Name + "\t" +item.MeanValue,
        //         BackgroundColor = Colors.Green
        //     };
        //     
        //     button.Clicked += async (sender, e) =>
        //     {
        //         await Navigation.PushAsync(new ItemPage(item));
        //     };
        //     
        //     return button;
        // }
    }