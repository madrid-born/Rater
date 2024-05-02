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
            const string connString = "Server=win2016-770ir.hostnegar.com\\MSSQLSERVER2022;Database=RaterDataBase;User ID=alireza;Password=15iX#6to0;TrustServerCertificate=True";
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

        public void AddItem(Item item)
        {
            Items.Add(item);
            SaveChanges();
        }

        //Checks Functions

        public bool CheckUserAuthentication(User user)
        {
            // TODO : make this function later
            return true;
        }

        public bool CheckUsernameInDatabase(string username)
        {
            // TODO : make this function later
            return true;
        }

        public List<Topic> GetTopicsForUser()
        {
            // TODO : make this function later
            var result = GetTopics().ToList();
            return result;
        }
    }