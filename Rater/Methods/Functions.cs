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

        // Serialize and Deserialize Types 
        public static string SerializeMeanValues(Dictionary<string, double> dictionary)
        {
            return JsonSerializer.Serialize(dictionary);
        }

        public static Dictionary<string, double> DeserializeMeanValues(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, double>>(json);
        }

        public static string SerializeValues(Dictionary<string, Dictionary<int, int>> dictionary)
        {
            return JsonSerializer.Serialize(dictionary);
        }

        public static Dictionary<string, Dictionary<int, int>> DeserializeValues(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<int, int>>>(json);
        }
        
        public static string SerializeStringList(List<string> list)
        {
            return JsonSerializer.Serialize(list);
        }

        public static List<string> DeserializeStringList(string json)
        {
            return JsonSerializer.Deserialize<List<string>>(json);
        }
        
        public static string SerializeIntList(List<int> list)
        {
            return JsonSerializer.Serialize(list);
        }

        public static List<int> DeserializeIntList(string json)
        {
            return JsonSerializer.Deserialize<List<int>>(json);
        }
    }