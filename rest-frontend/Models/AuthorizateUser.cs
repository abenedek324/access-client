/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * A felhasználó biztonsági adatainak definiálására szolgáló osztály
	\param Id int típusú azonoaítószám
	\param Username string típusú felhasználónév
	\param Password string típusú jelszó
	\param Card tring típusú azonosító kártya
	\param Pin string típusú PIN kód
	\param AccessTime DateTime típusú azonoaító a belépés idejének azonosítására
	\param Location string típusú helyszín
	\param Direction string típusú irány
    */
    public class AuthorizateUser
    {   public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Card { get; set; }
        public string Pin { get; set; }
        public DateTime AccessTime { get; set; }
        public string Location { get; set; }
        public string Direction { get; set; }

         /*!
         * A felhasználó adatainak példányosítása 1
         */
        public AuthorizateUser(int id, string username, string pass, string card, string pin, DateTime acc, string location, string direction)
        {
            Id = id;
            Username = username;
            Password = pass;
            Card = card;
            Pin = pin;
            AccessTime = acc;
            Location = location;
            Direction = direction;
        }
        /*!
         * A felhasználó adatainak példányosítása 2
         */
        public AuthorizateUser(int id, string password, DateTime accestime)
        {
            Id = id;
            Password = password;
            AccessTime=accestime;
        }

        /*!
         * A felhasználó adatainak példányosítása 3
         */
        public AuthorizateUser(string username, string password, DateTime accestime)
        {
            Username = username;
            Password = password;
            AccessTime = accestime;
        }

    }

    
}
