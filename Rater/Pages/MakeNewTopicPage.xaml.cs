using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rater.Pages ;

    public partial class MakeNewTopicPage : ContentPage
    {
        private List<Entry> _entries = new ();
        public MakeNewTopicPage()
        {
            InitializeComponent();
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
        //     var descriptionEntry = new Entry { Placeholder = "Description"};
        //     sl.Children.Add(descriptionEntry);
        //     
        //     var label = new Label { Text = "Attributes" };
        //     sl.Children.Add(label);
        //     
        //     var attributesLayout = new StackLayout();
        //     var addAttributesButton = new Button { Text = "Add Attributes" };
        //     addAttributesButton.Clicked += (sender, e) =>
        //     {
        //         var newAttributeEntry = new Entry{Text = ""};
        //         _entries.Add(newAttributeEntry);
        //         attributesLayout.Children.Add(newAttributeEntry);
        //     };
        //     sl.Children.Add(addAttributesButton);
        //     sl.Children.Add(attributesLayout);
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
        //         var description = "";
        //         if (!string.IsNullOrEmpty(descriptionEntry.Text))
        //         {
        //             description = descriptionEntry.Text;
        //         }
        //         var person = RDG.RandomPersonGeneratorTemp().Name;
        //         var attributes = new List<string>();
        //         foreach (var attributeEntry in _entries)
        //         {
        //             var attribute = attributeEntry.Text;
        //             if (string.IsNullOrEmpty(attribute))
        //             {
        //                 await DisplayAlert("Error", "One of attributes is empty", "OK");
        //                 _entries = new List<Entry>();
        //                 return;
        //             }
        //             attributes.Add(attribute);
        //         }
        //         if (_entries.Count == 0)
        //         {
        //             await DisplayAlert("Error", "Attributes cant be none", "OK");
        //             return;
        //         }
        //         var topic = new Topic(name, person, description, attributes);
        //         Functions.AddTopic(topic);
        //         MessagingCenter.Send(this, "UpdateTopicsPage", topic);
        //         await Navigation.PopAsync();
        //     };
        //     sl.Children.Add(submitButton);
        //
        //     Content = new ScrollView { Content = sl};
        // }
    }