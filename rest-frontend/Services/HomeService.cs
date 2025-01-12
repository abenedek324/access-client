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
            AuthorizateUser auth;
            int id;
            string username;
            try
            {
                id = Int32.Parse(userdata);
                auth = new AuthorizateUser(id, password, DateTime.Now);
            }
            catch
            {
                username = userdata;
                auth = new AuthorizateUser(username,password, DateTime.Now);
            }
            var json = JsonSerializer.Serialize(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("https://localhost:7124/api/login", content);
                response.EnsureSuccessStatusCode();

                int userId = -1;
                var responseContent = await response.Content.ReadAsStringAsync();
                userId = int.Parse(responseContent);
               
                return userId;
            }
            catch
            {
                return 0;
            }
        }
    }
}
