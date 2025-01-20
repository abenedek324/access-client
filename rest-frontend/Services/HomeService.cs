using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using rest_frontend.Models;
using System.Text;
using Microsoft.AspNetCore.Session;

namespace rest_frontend.Services
{
    public class HomeService
    {
        private readonly HttpClient _httpClient;
        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<int> Login(string userdata, string password)
        {
            //A bejelentkezéshez szükséges adatok lekérése
            AuthorizateUser auth;
            int id;
            string username;
            try
            {
                //Authuser példány létrehozása ID alapján
                id = Int32.Parse(userdata);
                auth = new AuthorizateUser(id, password, DateTime.Now);
            }
            catch
            {
                //Authuser példány létrehozása username alapján
                username = userdata;
                auth = new AuthorizateUser(username,password, DateTime.Now);
            }

            //Az authusert json kompatibilissé alakítjuk
            var json = JsonSerializer.Serialize(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                //Az authusert elküldjük az API-nak
                //Amennyiben a kérés sikeres visszkapjuk a userid-t és azt küldjük a controllernek
                var response = await _httpClient.PostAsync("https://localhost:7124/api/login", content);
                response.EnsureSuccessStatusCode();

                int userId = -1;
                var responseContent = await response.Content.ReadAsStringAsync();
                userId = int.Parse(responseContent);
               
                return userId;
            }
            catch
            {
                //Ha nincs user a megadott adatok alapján 0 user_id-t küldünk a controllernek
                return 0;
            }
        }
    }
}
