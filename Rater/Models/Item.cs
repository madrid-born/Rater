using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Rater.Methods;

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
        
        public double MeanValue { get; set; }
        
        public double MeanValueSum { get; set; }

        public string ValuesJson { get; set; }

        public string MeanValuesJson { get; set; }


        public void DefaultValues(Topic parent)
        {
            MeanValue = 0;
            MeanValueSum = 0;
            var values = new Dictionary<string, Dictionary<int, int>>();
            foreach (var user in parent.Members())
            {
                var dic = new Dictionary<int, int>();
                var list = parent.Attributes();
                for (var index = 0; index < list.Count; index++)
                {
                    dic.Add(index ,0);
                }
                values.Add(user, dic);
            }
            var meanValues = parent.Members().ToDictionary<string, string, double>(person => person, person => 0);
            ValuesJson = Functions.SerializeValues(values);
            MeanValuesJson = Functions.SerializeMeanValues(meanValues);
        }
    }