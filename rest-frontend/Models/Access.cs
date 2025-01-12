using System.Text.Json.Serialization;

namespace rest_frontend.Models
{
    public class Access
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("employeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("locationId")]
        public int LocationId { get; set; }

        [JsonPropertyName("accessTime")]
        public DateTime AccessTime { get; set; }

        [JsonPropertyName("direction")]
        public AccessDirection Direction { get; set; }
        public Access(int empId, int locId, DateTime accesTime, AccessDirection direction)
        {
            EmployeeId = empId;
            LocationId = locId;
            AccessTime = accesTime;
            Direction = direction;
        }
        public Access() { }
    }
    
}
