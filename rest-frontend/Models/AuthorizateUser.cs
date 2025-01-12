namespace rest_frontend.Models
{
    public class AuthorizateUser
    {   public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Card { get; set; }
        public string Pin { get; set; }
        public DateTime AccessTime { get; set; }
        public string Location { get; set; }
        public string Direction { get; set; }

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
        public AuthorizateUser(int id, string password, DateTime accestime)
        {
            Id = id;
            Password = password;
            AccessTime=accestime;
        }

        public AuthorizateUser(string username, string password, DateTime accestime)
        {
            Username = username;
            Password = password;
            AccessTime = accestime;
        }

    }

    
}
