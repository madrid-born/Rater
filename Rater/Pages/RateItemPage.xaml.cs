using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Methods;
using Rater.Models;

namespace Rater.Pages ;

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
            var sl = new StackLayout { Margin = 20, Spacing = 5};

            var list = _parentTopic.Attributes();
            for (var index = 0; index < list.Count; index++)
            {
                _userValues.Add(index, 0);
                sl.Children.Add(CreateVsl(list[index]));
            }
            
            var button = new Button
            {
                Text = "Proceed"
            };
            button.Clicked += (sender, e) =>
            {
                UpdateItem();
                Navigation.PopAsync();
            };
            sl.Add(button);
            
            Content = new ScrollView { Content = new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = sl}};
        }

        private VerticalStackLayout CreateVsl(string attribute)
        {
            var attributeLabel = new Label { Text = attribute };
            
            var table = new Grid 
            {
                ColumnSpacing = 5
            };
            table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            for (var i = 1; i <= 11; i++)
            {
                table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        
                if (i >= 11) continue;
                var numberLabel = new Label { Text = i.ToString()};
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

            var vsl = new VerticalStackLayout{Children = { attributeLabel, table }};
            return vsl;
        }
        
        private void UpdateItem()
        {
            try
            {
                var user = Functions.GetUsername();
                var values = Functions.DeserializeValues(_item.ValuesJson);
                var meanValues = Functions.DeserializeMeanValues(_item.MeanValuesJson);
                values[user] = _userValues;
                double sum = 0;
                var count = 0;
                var list = _parentTopic.Attributes();
                for (var index = 0; index < list.Count; index++)
                {
                    if (values[user][index] == 0) continue;
                    count++;
                    sum += values[user][index];
                }
                sum /= count;
                meanValues[user] = sum;
                _item.ValuesJson = Functions.SerializeValues(values);
                _item.MeanValuesJson = Functions.SerializeMeanValues(meanValues);
                _item.MeanValueSum += sum;
                _item.MeanValue = _item.MeanValueSum / _parentTopic.Members().Count(person => meanValues[person] != 0);
                _databaseContext.UpdateItem(_item);
            }
            catch (Exception e)
            {
                DisplayAlert("sss", e.Message, "ds");
            }
            
        }
    }