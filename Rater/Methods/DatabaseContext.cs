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

        public IEnumerable<User> GetUsers()
        {
            return Users.ToList();
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

        public void AddTopic(Topic topic)
        {
            Topics.Add(topic);
            SaveChanges();
        }

        public IEnumerable<Item> GetItems()
        {
            return Items.ToList();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
            SaveChanges();
        }

    }