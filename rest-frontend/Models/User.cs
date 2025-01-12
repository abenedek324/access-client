using System.Data;
using System.Text.Json.Serialization;


namespace rest_frontend.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("pin")]
        public string Pin { get; set; }

        [JsonPropertyName("card")]
        public string Card { get; set; }

        [JsonPropertyName("validationTypes")]
        public List<ValidationTypes> ValidationTypes { get; set; }
        
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("employee")]
        public Employee Employee { get; set; }

        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; }
        public User()
        {

        }
    }
}
