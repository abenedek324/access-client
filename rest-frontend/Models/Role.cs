using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    public class Role
    {
    /*!
    * A jogosultságokat definiáló osztály
    \param Id int típusú azonosító szám
    \param Name string típusú helyszín azonosító
    */
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        /*!
        *A jogosultság meghatározása paraméterben kapott név alapján
        \param Name
        \return Employee, Manager, vagy Admin
        */
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
