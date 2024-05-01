using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rater.Models ;

    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string OwnersName { get; set; }
        
        // public List<string> Attributes  { get; set; }
        // public List<Item> Items  { get; set; }
        // public List<string> Members  { get; set; }    
        
        public string AttributesJson { get; set; }
        public string ItemsJson { get; set; }
        public string MembersJson { get; set; }
        
        // public Topic(string name, string owner)
        // {
        //     Name = name;
        //     Owner = owner;
        //     AddMember(Owner);
        // }
        //
        // public Topic(string name, string owner, string description, List<string> attributes)
        // {
        //     Name = name;
        //     Owner = owner;
        //     Description = description;
        //     Attributes = attributes;
        //     AddMember(Owner);
        // }
    }