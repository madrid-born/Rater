using Rater.Methods;
using Rater.Models;

namespace Rater.Pages.Items_pages ;

    public partial class RateItemPage : ContentPage
    {
        private DatabaseContext _databaseContext;
        private int _itemId;
        private Item _item;
        private Topic _parentTopic;
        private Dictionary<int, int> _userValues = new ();
        public RateItemPage(DatabaseContext databaseContext, int itemId)
        {
            InitializeComponent();
            _databaseContext = databaseContext;
            _itemId = itemId;

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _item = _databaseContext.GetItemById(_itemId);
            _parentTopic = _databaseContext.GetTopicById(_item.ParentId);
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Margin = 20, Spacing = 20};

            var list = _parentTopic.Attributes();
            for (var index = 0; index < list.Count; index++)
            {
                _userValues.Add(index, 0);
                sl.Children.Add(CreateVsl(list[index]));
            }
            
            var button = new Button
            {
                Text = "Proceed",
                BackgroundColor = Colors.GreenYellow,
                TextColor = Colors.SaddleBrown,
                FontSize = 30
            };
            button.Clicked += (sender, e) =>
            {
                _item.SetValues(Functions.GetUsername(), _userValues, _parentTopic);
                _databaseContext.UpdateItem(_item);
                Navigation.PopAsync();
            };
            sl.Children.Add(button);
            
            Content = new ScrollView { Content = new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = sl}};
        }

        private VerticalStackLayout CreateVsl(string attribute)
        {
            var attributeLabel = new Label
            {
                Text = attribute,
                TextColor = Colors.Black,
                FontSize = 20
            };
            
            var table = new Grid 
            {
                ColumnSpacing = -15,
                Padding = 10
            };
            table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            for (var i = 1; i <= 11; i++)
            {
                table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        
                if (i >= 11) continue;
                var numberLabel = new Label
                {
                    Text = i.ToString(),
                    Margin = i==10 ? new Thickness(4, 0, 0, 0) : new Thickness(8, 0, 0, 0)
                };
                table.Children.Add(numberLabel);
                table.SetRow(numberLabel, 0);
                table.SetColumn(numberLabel, i);
                
                var radioButton = new RadioButton
                {
                    BindingContext = (_parentTopic.Attributes().IndexOf(attribute), i),
                };
                radioButton.CheckedChanged += (sender, e) =>
                {
                    var radioButtonSender = (RadioButton)sender;
                    var(attr, value) = (ValueTuple<int, int>)radioButtonSender.BindingContext;
                    _userValues[attr] = value;
                };
                
                table.Children.Add(radioButton);
                table.SetRow(radioButton, 1);
                table.SetColumn(radioButton, i);
            }

            var vsl = new VerticalStackLayout
            {
                Spacing = 10,
                BackgroundColor = Colors.DarkGray,
                Padding = 10,
                Children = { attributeLabel, table }
            };
            return vsl;
        }
    }