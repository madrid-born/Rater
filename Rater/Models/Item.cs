using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Rater.Models ;

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ParentId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public double MeanValue { get; set; } = 0;
        
        public double MeanValueSum { get; set; } = 0;
        
        public string ValuesJson { get; set; }
        
        public string MeanValuesJson { get; set; }
        
        
        // public Item(string name, Topic parent)
        // {
        //     Name = name;
        //     Parent = parent;
        //     DateCreated = DateTime.Now;
        //     foreach (var person in parent.Members)
        //     {
        //         var dic = parent.Attributes.ToDictionary(attribute => attribute, attribute => 0);
        //         Values.Add(person, dic);
        //
        //     }
        //     foreach (var person in parent.Members)
        //     {
        //         MeanValues.Add(person, 0);
        //     }
        // }
        
    }