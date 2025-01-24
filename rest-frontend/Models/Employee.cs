using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * Dolgozó mozgásának definiálására szolgáló osztály
	\param Id int típusú azonoaítószám
	\param Active bool típusú meghatározás a belépésre
	\param JoiningDate DateTime típusú időbeállítás - a belépés ideje
	\param UserId int típusú azonosító
	\param AuthorizedLocations lista típusú engedélyezett belépési helyek
	\param Accesses lista típusú jogosultságok azonosítására
    */
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

         /*!
         * Dolgozó belépése
         */
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
