using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
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
        
        public Dictionary<string, double> MeanValues()
        {
            return Functions.DeserializeMeanValues(MeanValuesJson);
        }
        
        public Dictionary<string, Dictionary<int, int>> Values()
        {
            return Functions.DeserializeValues(ValuesJson);
        }

        public void SetValues(string user, Dictionary<int, int> userValues, Topic parentTopic)
        {
            var values = Values();
            var meanValues = MeanValues();
            values[user] = userValues;
            double sum = 0;
            var count = 0;
            var list = parentTopic.Attributes();
            for (var index = 0; index < list.Count; index++)
            {
                if (values[user][index] == 0) continue;
                count++;
                sum += values[user][index];
            }
            sum /= count;
            meanValues[user] = sum;
            ValuesJson = Functions.SerializeValues(values);
            MeanValuesJson = Functions.SerializeMeanValues(meanValues);
            MeanValueSum += sum;
            MeanValue = MeanValueSum / parentTopic.Members().Count(person => meanValues[person] != 0);
        }
        
        public void AddUser(string username)
        {
            // TODO : make later
        }
    }