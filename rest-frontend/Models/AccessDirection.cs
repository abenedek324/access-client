using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
     * Az irány definiálására szolgáló osztály
     */
    public class AccessDirection
    {
        
        /*!
			\param name string típusú azonosító
			\return Irány definiálása szolgál.
			\sa Access, AccesDirection, AccesDirection()
		*/
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /*!
         * Az irány nevének példányosítása
         */
        public AccessDirection(string name)
        {
            Name = name;
        }
        
         /*!
         * Az irány meghívása
         */
        public AccessDirection() { }
    }
}
