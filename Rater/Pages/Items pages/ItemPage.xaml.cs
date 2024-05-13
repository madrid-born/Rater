using Rater.Methods;
using Rater.Models;

namespace Rater.Pages.Items_pages ;

    public partial class ItemPage : ContentPage
    {
        private DatabaseContext _databaseContext;
        private int _itemId;
        private Item _item;
        private Topic _parentTopic;
        
        public ItemPage(DatabaseContext databaseContext, int itemId)
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

            var vsl = new StackLayout { Spacing = 20, Padding = 20 , BackgroundColor= Colors.BlanchedAlmond};
            
            vsl.Children.Add(CreateHsl("Topic", _parentTopic.Name));
            vsl.Children.Add(CreateHsl("Name", _item.Name));
            vsl.Children.Add(CreateHsl("Total Mean Value", _item.MeanValue.ToString()));
            foreach (var person in _parentTopic.Members())
            {
                vsl.Children.Add(CreateHsl(person, _item.MeanValues()[person].ToString()));
            }
            sl.Children.Add(vsl);
            
            if (_item.MeanValues()[Functions.GetUsername()] == 0)
            {
                sl.Children.Add(CreateRateButton());
            }
            
            sl.Children.Add(new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = CreateGrid()});
            
            Content = new ScrollView { Content = sl};
        }
        
        private HorizontalStackLayout CreateHsl(string constant, string value)
        {
            var layout = new HorizontalStackLayout
            {
                Children = { new Label
                {
                    Text = $"{constant} : {value}",
                    TextColor = Colors.Black,
                    FontSize = 20
                }}
            };
        
            return layout;
        }
        
        private Grid CreateGrid()
        {
            var attributes = _parentTopic.Attributes();
            var members = _parentTopic.Members();
            var values = _item.Values();
            var table = new Grid 
            {
                Padding = 20,
                RowSpacing = 10,
                ColumnSpacing = 10,
                BackgroundColor = Colors.BlanchedAlmond
               
            };
            for (var i = 0; i < members.Count + 1; i++)
            {
                table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
        
            var attributeColumn = new VerticalStackLayout();
            table.Children.Add(attributeColumn);
            table.SetColumn(attributeColumn, 0);
            attributeColumn.Children.Add(new Label
            {
                Text = "Attributes",
                TextColor = Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center
            });

            foreach (var attribute in attributes)
            {
                attributeColumn.Children.Add(new Label
                {
                    Text = attribute,
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.End
                });
            }
        
            for (var i = 0; i < members.Count; i++)
            {
                var personColumn = new VerticalStackLayout();
                table.Children.Add(personColumn);
                table.SetColumn(personColumn, i+1);
                personColumn.Children.Add(new Label
                {
                    Text = members[i],
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.Center
                });
                foreach (var attribute in attributes)
                {
                    personColumn.Children.Add(new Label
                    {
                        Text = values[members[i]][attributes.IndexOf(attribute)].ToString(),
                        TextColor = Colors.Black,
                        HorizontalTextAlignment = TextAlignment.Center
                    });
                }
            }
        
            return table;
        }
        
        private Button CreateRateButton()
        {
            var addRateButton = new Button
            {
                Text = "Add Your Rate",
                BackgroundColor = Colors.GreenYellow,
                TextColor = Colors.SaddleBrown,
                FontSize = 30
            };
            addRateButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new RateItemPage(_databaseContext, _itemId));
            };
            return addRateButton;
        }
    }