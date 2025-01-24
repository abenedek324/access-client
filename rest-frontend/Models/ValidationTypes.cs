using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * Az igazolási lehetőségeket definiáló osztály
    \param Id int típusú azonosító szám
    \param Name string típusú megnevezés
    */
    public class ValidationTypes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public ValidationTypes() {
            
        }
        /*!
        *Az igazolási lehetőség meghatározása paraméterben kapott név alapján
        \param Name
        \return Pin, Card, vagy Password
        */
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
