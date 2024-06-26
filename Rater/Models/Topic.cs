﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rater.Methods;

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

        public string AttributesJson { get; set; }
        public string ItemsIdJson { get; set; } = Functions.SerializeIntList(new List<int>());
        public string MembersJson { get; set; }

        public List<string> Attributes()
        {
            return Functions.DeserializeStringList(AttributesJson);
        }

        public List<int> ItemsId()
        {
            return Functions.DeserializeIntList(ItemsIdJson);
        }

        public void AddItemToTopic(int itemId)
        {
            var items = ItemsId();
            items.Add(itemId);
            ItemsIdJson = Functions.SerializeIntList(items);
        }
        
        public List<string> Members()
        {
            return Functions.DeserializeStringList(MembersJson);
        }

        public void AddMember(DatabaseContext databaseContext, string username)
        {
            var members = Members();
            members.Add(username);
            MembersJson = Functions.SerializeStringList(members);
            foreach (var item in ItemsId().Select(databaseContext.GetItemById))
            {
                item.AddUser(username, Attributes());
                databaseContext.UpdateItem(item);
            }
        }
    }