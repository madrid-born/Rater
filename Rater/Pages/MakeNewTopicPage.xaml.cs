using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

    public partial class MakeNewTopicPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private List<Entry> _entries = new ();
        
        public MakeNewTopicPage(DatabaseContext dbContext)
        {
            InitializeComponent();
            _databaseContext = dbContext;
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
            
            var descriptionEntry = new Entry { Placeholder = "Description"};
            sl.Children.Add(descriptionEntry);
            
            var label = new Label { Text = "Attributes" };
            sl.Children.Add(label);
            
            var attributesLayout = new StackLayout();
            var addAttributesButton = new Button { Text = "Add Attributes" };
            addAttributesButton.Clicked += (sender, e) =>
            {
                var newAttributeEntry = new Entry{Text = ""};
                _entries.Add(newAttributeEntry);
                attributesLayout.Children.Add(newAttributeEntry);
            };
            sl.Children.Add(addAttributesButton);
            sl.Children.Add(attributesLayout);
        
            var submitButton = new Button { Text = "Submit"};
            submitButton.Clicked += async (sender, e) =>
            {
                var name = nameEntry.Text;
                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Error", "Name can't be empty", "OK");
                    return;
                }
                var description = "";
                if (!string.IsNullOrEmpty(descriptionEntry.Text))
                {
                    description = descriptionEntry.Text;
                }
                var ownersName = Functions.GetUsername();
                var attributes = new List<string>();
                foreach (var attribute in _entries.Select(attributeEntry => attributeEntry.Text))
                {
                    if (string.IsNullOrEmpty(attribute))
                    {
                        await DisplayAlert("Error", "One of attributes is empty", "OK");
                        _entries = new List<Entry>();
                        return;
                    }
                    attributes.Add(attribute);
                }
                if (_entries.Count == 0)
                {
                    await DisplayAlert("Error", "Attributes cant be none", "OK");
                    return;
                }
                var topic = new Topic {Name = name, OwnersName = ownersName, Description = description, AttributesJson = Functions.SerializeStringList(attributes)};
                // var topic = new Topic(name, ownersName, description, attributes);
                _databaseContext.AddTopic(topic);
                // Functions.AddTopic(topic);
                MessagingCenter.Send(this, "UpdateTopicsPage", topic);
                await Navigation.PopAsync();
            };
            sl.Children.Add(submitButton);
            Content = new ScrollView { Content = sl};
        }
    }