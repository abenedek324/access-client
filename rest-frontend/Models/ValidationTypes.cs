using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class ValidationTypes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public ValidationTypes() {
            
        }
        public ValidationTypes(string name)
        {
            Name = name;
            switch (name)
            {
                case "Card":
                    Id = 2;
                    break;
                case "Pin":
                    Id = 1;
                    break;
                case "Password":
                    Id = 3;
                    break;
            }
        }
    }
}
