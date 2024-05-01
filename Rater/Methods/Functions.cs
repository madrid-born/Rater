using System.Text.Json;
using Rater.Models;

namespace Rater.Methods ;

    public static class Functions
    {
        
        
        
        public static void AuthorizeUser(User user)
        {
            Preferences.Set("Username",user.Name);
        }
        
        public static string GetUsername()
        {
            return Preferences.Get("Username", "");
        }
        
        // Database Methods
        
        // public static List<string> UsernamesInDatabase()
        // {
        //     var result = new List<string>();
        //     return result;
        // }
        //
        // public static void AddUserToDatabase(Person person)
        // {
        //     
        // }
        //
        // public static bool CheckUSerInDatabase(Person person)
        // {
        //     return true;
        // }
        //
        // public static List<Topic> GetUserTopics()
        // {
        //     var username = GetUsername();
        //     return RDG.TopicListGeneratorTemp(10);
        // }
        //
        // public static void AddTopic(Topic topic)
        // {
        //     
        // }
        //
        // public static void AddItem(Item item)
        // {
        //     
        // }
        
        
        // Serialize and Deserialize Types 
        public static string SerializeMeanValues(Dictionary<string, double> dictionary)
        {
            return JsonSerializer.Serialize(dictionary);
        }

        public static Dictionary<string, double> DeserializeMeanValues(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, double>>(json);
        }

        public static string SerializeMeanValue(Dictionary<string, Dictionary<string, int>> dictionary)
        {
            return JsonSerializer.Serialize(dictionary);
        }

        public static Dictionary<string, Dictionary<string, int>> DeserializeMeanValue(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(json);
        }
        
        public static string SerializeStringList(List<string> list)
        {
            return JsonSerializer.Serialize(list);
        }

        public static List<string> DeserializeStringList(string json)
        {
            return JsonSerializer.Deserialize<List<string>>(json);
        }
    }