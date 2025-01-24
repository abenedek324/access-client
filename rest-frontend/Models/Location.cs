using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * A belépés helyét definiáló osztály
    \param Id int típusú azonosító szám
    \param Name string típusú helyszín azonosító
    \param Address string típusú helyszín azonosító
    */
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

        /*!
        *A helyszín meghatározása paraméterben kapott név alapján
        \param Name
        \return HQ vagy Office
        */        
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
