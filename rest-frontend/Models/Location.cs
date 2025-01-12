using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class Location
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
        public Location()
        {

        }

        public Location(string name)
        {
            switch (name) 
            {
                case "HQ":
                    Id = 1;
                    break;
                case "Office":
                    Id = 2;
                    break;
            }
            Name = name;

        }
    }
}
