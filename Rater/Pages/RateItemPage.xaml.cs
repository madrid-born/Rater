using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rater.Models;

namespace Rater.Pages ;

    public partial class RateItemPage : ContentPage
    {
        private string _person;
        private Item _item;
        public RateItemPage(Item item, string person)
        {
            InitializeComponent();
            _person = person;
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
        //     foreach (var vsl in _item.Parent.Attributes.Select(attribute => CreateVsl(attribute)))
        //     {
        //         sl.Children.Add(vsl);
        //     }
        //
        //     var button = new Button
        //     {
        //         Text = "Proceed"
        //     };
        //     button.Clicked += (sender, e) =>
        //     {
        //         _item.MeanValueCalculator(_person);
        //         Navigation.PopAsync();
        //     };
        //     sl.Add(button);
        //     
        //     Content = new ScrollView { Content = new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = sl}};
        // }
        //
        // private VerticalStackLayout CreateVsl(string attribute)
        // {
        //     var attributeLabel = new Label { Text = attribute };
        //     
        //     var table = new Grid 
        //     {
        //         ColumnSpacing = 5
        //     };
        //     table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //     table.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //     
        //     for (var i = 1; i <= 11; i++)
        //     {
        //         table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        //
        //         if (i >= 11) continue;
        //         var numberLabel = new Label { Text = i.ToString()};
        //         table.Children.Add(numberLabel);
        //         table.SetRow(numberLabel, 0);
        //         table.SetColumn(numberLabel, i);
        //         
        //         var radioButton = new RadioButton
        //         {
        //             BindingContext = (attribute, i),
        //         };
        //         radioButton.CheckedChanged += (sender, e) =>
        //         {
        //             var radioButtonSender = (RadioButton)sender;
        //             var(attr, value) = (ValueTuple<string, int>)radioButtonSender.BindingContext;
        //             _item.AddValue(_person, attr, value);
        //         };
        //         
        //         table.Children.Add(radioButton);
        //         table.SetRow(radioButton, 1);
        //         table.SetColumn(radioButton, i);
        //     }
        //     
        //     // var attributeHsl = new HorizontalStackLayout();
        //     // for (var i = 1; i <= 10; i++)
        //     // {
        //     //     var radioButton = new RadioButton
        //     //     {
        //     //         BindingContext = (attribute, i)
        //     //     };
        //     //     radioButton.CheckedChanged += (sender, e) =>
        //     //     {
        //     //         var radioButtonSender = (RadioButton)sender;
        //     //         var(attr, value) = (ValueTuple<string, int>)radioButtonSender.BindingContext;
        //     //         _item.AddValue(_person, attr, value);
        //     //     };
        //     //     attributeHsl.Children.Add(radioButton);
        //     // }
        //         
        //     var vsl = new VerticalStackLayout{Children = { attributeLabel, table }};
        //     return vsl;
        // }
    }