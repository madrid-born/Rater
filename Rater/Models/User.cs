using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rater.Methods;

namespace Rater.Models ;

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }
        
        public string TopicsIdIncludedJson { get; set; }

        public IEnumerable<int> TopicsIncluded()
        {
            return Functions.DeserializeIntList(TopicsIdIncludedJson);
        }

        public void AddToTopics(int topicId)
        {
            var topics = Functions.DeserializeIntList(TopicsIdIncludedJson);
            topics.Add(topicId);
            TopicsIdIncludedJson = Functions.SerializeIntList(topics);
        }
    }
    