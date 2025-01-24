using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
     * A hozzáférés tulajdonságai Json Propertykben
     */
    public class Access
    {
         /*!
			\param id int típusú azonoaítószám
			\return Az ID azonosítót határozza meg.
		*/
        [JsonPropertyName("id")]
        public int Id { get; set; }

        
        /*!
			\param employeeId int típusú azonoaítószám
			\return A dolgozó azonosítására szolgál.
			\sa Access
		*/
        [JsonPropertyName("employeeId")]
        public int EmployeeId { get; set; }

        /*!
			\param locationId int típusú azonoaítószám
			\return a helyszín azonosítására szolgál.
			\sa Access
		*/
        [JsonPropertyName("locationId")]
        public int LocationId { get; set; }

         /*!
			\param accessTime DateTime típusú azonoaító
			\return  a belépés idejének azonosítására szolgál.
			\sa Access
		*/
        [JsonPropertyName("accessTime")]
        public DateTime AccessTime { get; set; }

         /*!
			\param direction külön osztályban
			\return Irány definiálása szolgál.
			\sa Access, AccesDirection
		*/
        [JsonPropertyName("direction")]
        public AccessDirection Direction { get; set; }
        
        /*!
         * Konstruktor inicializálja a hozzáférést
		 \sa EmployeeId, LocationId, AccessTime, Direction
         */
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
