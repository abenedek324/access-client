using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class AccessDirection
    {
        

        [JsonPropertyName("name")]
        public string Name { get; set; }

        
        public AccessDirection(string name)
        {
            Name = name;
                    }
        public AccessDirection() { }
    }
}
