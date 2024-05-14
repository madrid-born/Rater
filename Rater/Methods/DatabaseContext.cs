using Microsoft.EntityFrameworkCore;
using Rater.Models;

namespace Rater.Methods ;

    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Item> Items { get; set; }

        public DatabaseContext()
        {
            
        }
        
        public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connString = "Server=158.58.187.131\\MSSQLSERVER2022;Database=RaterDataBase;User ID=alireza;Password=15iX#6to0;Encrypt=False;Persist Security Info=True;TrustServerCertificate=True;";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connString);
            }
        }
        
        //crud

        public IEnumerable<User> GetUsers()
        {
            return Users.ToList();
        }
        
        public User GetUserById(int userId)
        {
            return GetUsers().FirstOrDefault(user => user.Id == userId);
        }

        public User GetUserByName(string username)
        {
            return GetUsers().FirstOrDefault(user => user.Name == username);
        }

        public void UpdateUser(User user)
        {
            Users.Update(user);
            SaveChanges();
        }
        
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
        
        public IEnumerable<Topic> GetTopics()
        {
            return Topics.ToList();
        }
        
        public Topic GetTopicById(int topicId)
        {
            return GetTopics().FirstOrDefault(topic => topic.Id == topicId);
        }

        public void UpdateTopic(Topic topic)
        {
            Topics.Update(topic);
            SaveChanges();
        }
        
        public void AddTopic(Topic topic)
        {
            Topics.Add(topic);
            SaveChanges();
        }

        public IEnumerable<Item> GetItems()
        {
            return Items.ToList();
        }

        public Item GetItemById(int itemId)
        {
            return GetItems().FirstOrDefault(item => item.Id == itemId);
        }
        
        public void UpdateItem(Item item)
        {
            Items.Update(item);
            SaveChanges();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
            SaveChanges();
        }

        //Checks Functions

        public bool CheckUserAuthentication(User user)
        {
            try
            {
                var person = GetUserByName(user.Name);
                return person.Password != user.Password;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public bool CheckUsernameInDatabase(string username)
        {
            try
            {
                var person = GetUserByName(username);
                return person.Id <= 0;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public List<Topic> GetTopicsForUser()
        {
            var user = GetUserByName(Functions.GetUsername());
            var idList = user.TopicsIncluded();
            return idList.Select(GetTopicById).ToList();
        }

        public List<Item> GetItemsForTopic(int topicId)
        {
            var parentTopic = GetTopicById(topicId);
            var idList = Functions.DeserializeIntList(parentTopic.ItemsIdJson);
            return idList.Select(GetItemById).ToList();
        }
    }
