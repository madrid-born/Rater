using Rater.Methods;
using Rater.Models;

namespace Rater.Pages.Topics_Pages ;

    public partial class MakeNewTopicPage : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private List<Entry> _entries = new ();
        private int _attributeCounter = 1;
        
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
                Spacing = 20,
                Margin = 20
            };
            
            var nameEntry = new Entry
            {
                Placeholder = "Name",
                BackgroundColor = Colors.BlanchedAlmond,
                TextColor = Colors.Black,
                FontSize = 20
            };
            sl.Children.Add(nameEntry);
            
            var descriptionEntry = new Entry
            {
                Placeholder = "Description",
                BackgroundColor = Colors.BlanchedAlmond,
                TextColor = Colors.Black,
                FontSize = 20
            };
            sl.Children.Add(descriptionEntry);
            
            var label = new Label
            {
                Text = "Attributes",
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 30
            };
            sl.Children.Add(label);
            
            var attributesLayout = new StackLayout();
            var addAttributesButton = new Button
            {
                Text = "Add Attribute",
                BackgroundColor = Colors.GreenYellow,
                TextColor = Colors.SaddleBrown,
                FontSize = 20
            };
            addAttributesButton.Clicked += (sender, e) =>
            {
                _attributeCounter++;
                var newAttributeEntry = new Entry
                {
                    Placeholder="Attribute " + _attributeCounter,
                    BackgroundColor = Colors.CadetBlue,
                    TextColor = Colors.Black,
                    FontSize = 20
                };
                _entries.Add(newAttributeEntry);
                attributesLayout.Children.Add(newAttributeEntry);
            };
            
            var newAttributeEntry = new Entry
            {
                Placeholder="Attribute 1",
                BackgroundColor = Colors.CadetBlue,
                TextColor = Colors.Black,
                FontSize = 20
            };
            _entries.Add(newAttributeEntry);
            attributesLayout.Children.Add(newAttributeEntry);
            
            sl.Children.Add(addAttributesButton);
            sl.Children.Add(attributesLayout);
        
            var submitButton = new Button
            {
                Text = "Submit",
                BackgroundColor = Colors.Orange,
                TextColor = Colors.SaddleBrown,
                FontSize = 20
            };
            submitButton.Clicked += async (sender, e) =>
            {
                try
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
                    var topic = new Topic {Name = name, OwnersName = ownersName, Description = description, AttributesJson = Functions.SerializeStringList(attributes), MembersJson = Functions.SerializeStringList(new List<string>{ownersName})};
                    _databaseContext.AddTopic(topic);
                    var user = _databaseContext.GetUserByName(ownersName);
                    user.AddToTopics(topic.Id);
                    _databaseContext.UpdateUser(user);
                    await Navigation.PopAsync();

                }
                catch (Exception exception)
                {
                    await DisplayAlert("ds", exception.Message, "dsd");
                }
                
            };
            sl.Children.Add(submitButton);
            Content = new ScrollView { Content = sl};
        }
    }