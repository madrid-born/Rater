using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Models;

namespace Rater.Pages ;

    public partial class MakeNewItemPage : ContentPage
    {
        private Topic _topic;
        public MakeNewItemPage(Topic topic)
        {
            InitializeComponent();
            _topic = topic;
        }
        
        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     FillTheFront();
        // }
        //
        // private void FillTheFront()
        // {
        //     var sl = new StackLayout
        //     {
        //         Spacing = 10,
        //         Margin = 20
        //     };
        //     
        //     var nameEntry = new Entry { Placeholder = "Name"};
        //     sl.Children.Add(nameEntry);
        //
        //     var submitButton = new Button { Text = "Submit"};
        //     submitButton.Clicked += async (sender, e) =>
        //     {
        //         var name = nameEntry.Text;
        //         if (string.IsNullOrEmpty(name))
        //         {
        //             await DisplayAlert("Error", "Name can't be empty", "OK");
        //             return;
        //         }
        //         
        //         var item = new Item(name, _topic);
        //         Functions.AddItem(item);
        //         MessagingCenter.Send(this, "UpdateTopicItemsPage", item);
        //         await Navigation.PopAsync();
        //     };
        //     sl.Children.Add(submitButton);
        //
        //     Content = new ScrollView { Content = sl};
        // }
    }