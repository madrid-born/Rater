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
        
        public string InvitedTopicsIdJson { get; set; }

        public List<int> TopicsIncluded()
        {
            return Functions.DeserializeIntList(TopicsIdIncludedJson);
        }
        
        public void AddToTopics(int topicId)
        {
            var topics = TopicsIncluded();
            topics.Add(topicId);
            TopicsIdIncludedJson = Functions.SerializeIntList(topics);
        }

        public List<int> TopicsInvited()
        {
            return Functions.DeserializeIntList(InvitedTopicsIdJson);
        }

        public void InviteToTopic(int topicId)
        {
            var invitedList = TopicsInvited();
            invitedList.Add(topicId);
            InvitedTopicsIdJson = Functions.SerializeIntList(invitedList);
        }

        public void AcceptInvite(int topicId)
        {
            var invitedList = TopicsInvited();
            var includedList = TopicsIncluded();
            invitedList.Remove(topicId);
            includedList.Add(topicId);
            InvitedTopicsIdJson = Functions.SerializeIntList(invitedList);
            TopicsIdIncludedJson = Functions.SerializeIntList(includedList);
        }
     
        public void DeclineInvite(int topicId)
        {
            var invitedList = TopicsInvited();
            invitedList.Remove(topicId);
            InvitedTopicsIdJson = Functions.SerializeIntList(invitedList);
        }
    }
    