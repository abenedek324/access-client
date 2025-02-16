﻿using System.Data;
using System.Text.Json.Serialization;

/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * A felhasználót definiáló osztály
    \param Id int típusú azonosító szám
    \param Username string típusú felhasználónév
    \param Password string típusú jelszó
    \param Pin string típusú PIN kód
    \param Card string típusú kártya azonosító
    \param ValidationTypes lista típusú igazolási lehetőség
    \param FirstName string típusú vezetésknév
    \param Last Name streing típusú keresztnév
    \param BirthDate DateTime típusú születési dátum
    \param Employee Employee() típusú dolgozó
    \param Roles lista típusú jogosultságok
    */
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
