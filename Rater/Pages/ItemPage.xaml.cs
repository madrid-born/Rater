using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

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
            _item = _databaseContext.GetItemById(_itemId);
            _parentTopic = _databaseContext.GetTopicById(_item.ParentId);
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }
        
        private void FillTheFront()
        {
            var sl = new StackLayout { Margin = 20, Spacing = 5};
            
            sl.Children.Add(CreateHsl("Topic", _parentTopic.Name));
            sl.Children.Add(CreateHsl("Name", _item.Name));
            sl.Children.Add(CreateHsl("Total Mean Value", _item.MeanValue.ToString()));
            foreach (var person in Functions.DeserializeStringList(_parentTopic.MembersJson))
            {
                sl.Children.Add(CreateHsl(person, Functions.DeserializeMeanValues(_item.MeanValuesJson)[person].ToString()));
            }
            sl.Children.Add(CreateRateButton());
            sl.Children.Add(new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = CreateGrid()});
            
            Content = new ScrollView { Content = sl};
        }
        
        private HorizontalStackLayout CreateHsl(string constant, string value)
        {
            var valueLabel = new Label
            {
                Text = value
            };
            var layout = new HorizontalStackLayout
            {
                Children = { new Label{Text = $"{constant} : "}, valueLabel}
            };
        
            return layout;
        }
        
        private Grid CreateGrid()
        {
            var attributes = Functions.DeserializeStringList(_parentTopic.AttributesJson);
            var members = Functions.DeserializeStringList(_parentTopic.MembersJson);
            var values = Functions.DeserializeValues(_item.ValuesJson);
            var table = new Grid 
            {
                ColumnSpacing = 10
            };
            for (var i = 0; i < members.Count + 1; i++)
            {
                table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
        
            var attributeColumn = new VerticalStackLayout();
            table.Children.Add(attributeColumn);
            table.SetColumn(attributeColumn, 0);
            attributeColumn.Children.Add(new Label { Text = "Attributes", HorizontalTextAlignment = TextAlignment.Center });

            foreach (var attribute in attributes)
            {
                attributeColumn.Children.Add(new Label { Text = attribute, HorizontalTextAlignment = TextAlignment.Center });
            }
        
            for (var i = 0; i < members.Count; i++)
            {
                var personColumn = new VerticalStackLayout();
                table.Children.Add(personColumn);
                table.SetColumn(personColumn, i+1);
                personColumn.Children.Add(new Label { Text = members[i], HorizontalTextAlignment = TextAlignment.Center });
                foreach (var attribute in attributes)
                {
                    personColumn.Children.Add(new Label { Text = values[members[i]][attributes.IndexOf(attribute)].ToString(), HorizontalTextAlignment = TextAlignment.Center });
                }
            }
        
            return table;
        }
        
        private Button CreateRateButton()
        {
            var addRateButton = new Button
            {
                Text = "Add Your Rate"
            };
            addRateButton.Clicked += async (sender, e) =>
            {
                // await Navigation.PushAsync(new RateItemPage(_item, Functions.GetUsername()));
            };
            return addRateButton;
        }
    }