using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class Employee
    {

        [JsonPropertyName("id")] 
        public int Id { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("joiningdate")]
        public DateTime JoiningDate { get; set; }

        [JsonPropertyName("userid")]
        public int UserId { get; set; }

        [JsonPropertyName("authorizedLocations")]
        public List<Location> AuthorizedLocations { get; set; }

        [JsonPropertyName("accesses")]
        public List<Access> Accesses { get; set; }


        public Employee(bool active, DateTime joiningdate, List<Location>authlocation)
        {
            Active = active;
            JoiningDate = joiningdate;
            AuthorizedLocations = authlocation;
        }
        public Employee()
        {
            
        }
    }
}
