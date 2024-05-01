using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Models;

namespace Rater.Pages ;

    public partial class ItemPage : ContentPage
    {
        private Item _item;
        
        public ItemPage(Item item)
        {
            InitializeComponent();
            _item = item;
        }
        
        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     FillTheFront();
        // }
        //
        // private void FillTheFront()
        // {
        //     var sl = new StackLayout { Margin = 20, Spacing = 5};
        //     
        //     sl.Children.Add(CreateHsl("Topic", _item.Parent.Name));
        //     sl.Children.Add(CreateHsl("Name", _item.Name));
        //     sl.Children.Add(CreateHsl("Total Mean Value", _item.MeanValue.ToString()));
        //     foreach (var person in _item.Parent.Members)
        //     {
        //         sl.Children.Add(CreateHsl(person, _item.MeanValues[person].ToString()));
        //     }
        //     sl.Children.Add(CreateRateButton());
        //     sl.Children.Add(new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = CreateGrid()});
        //     
        //     Content = new ScrollView { Content = sl};
        // }
        //
        // private HorizontalStackLayout CreateHsl(string constant, string value)
        // {
        //     var valueLabel = new Label
        //     {
        //         Text = value
        //     };
        //     var layout = new HorizontalStackLayout
        //     {
        //         Children = { new Label{Text = $"{constant} : "}, valueLabel}
        //     };
        //
        //     return layout;
        // }
        //
        // private Grid CreateGrid()
        // {
        //     var table = new Grid 
        //     {
        //         ColumnSpacing = 10
        //     };
        //     for (var i = 0; i < _item.Parent.Members.Count + 1; i++)
        //     {
        //         table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        //     }
        //
        //     var attributeColumn = new VerticalStackLayout();
        //     table.Children.Add(attributeColumn);
        //     table.SetColumn(attributeColumn, 0);
        //     attributeColumn.Children.Add(new Label { Text = "Attributes", HorizontalTextAlignment = TextAlignment.Center });
        //     foreach (var attribute in _item.Parent.Attributes)
        //     {
        //         attributeColumn.Children.Add(new Label { Text = attribute, HorizontalTextAlignment = TextAlignment.Center });
        //     }
        //
        //     for (var i = 0; i < _item.Parent.Members.Count; i++)
        //     {
        //         var personColumn = new VerticalStackLayout();
        //         table.Children.Add(personColumn);
        //         table.SetColumn(personColumn, i+1);
        //         personColumn.Children.Add(new Label { Text = _item.Parent.Members[i], HorizontalTextAlignment = TextAlignment.Center });
        //         foreach (var attribute in _item.Parent.Attributes)
        //         {
        //             personColumn.Children.Add(new Label { Text = _item.Values[_item.Parent.Members[i]][attribute].ToString(), HorizontalTextAlignment = TextAlignment.Center });
        //         }
        //     }
        //
        //     return table;
        // }
        //
        // private Button CreateRateButton()
        // {
        //     var addRateButton = new Button
        //     {
        //         Text = "Add Your Rate"
        //     };
        //     addRateButton.Clicked += async (sender, e) =>
        //     {
        //         await Navigation.PushAsync(new RateItemPage(_item, _item.Parent.Members[0]));
        //         // await Navigation.PushAsync(new RateItemPage(_item, Functions.GetUsername()));
        //     };
        //     return addRateButton;
        // }
    }