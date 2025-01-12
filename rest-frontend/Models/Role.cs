using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class Role
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public Role(string name)
        {
            switch (name) {
                case "Employee":
                    Id = 1;
                    Name = name;
                    break;
                case "Manager":
                    Id = 2;
                    Name = name;
                    break;
                case "Admin":
                    Id = 3;
                    Name = name;
                    break;
                            }
        }
    }
}
