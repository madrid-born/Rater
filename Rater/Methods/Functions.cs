using System.Text.Json;

namespace Rater.Methods ;

    public static class Functions
    {
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
    }