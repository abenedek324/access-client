using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using rest_frontend.Models;

namespace rest_frontend.Services
{
    public class EmployeeService
    {

        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       public async Task<User> GetSingleUser(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7124/api/employee/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<User>(json);


        }
        public async Task<List<User>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7124/api/employee/all");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(json);
            return JsonSerializer.Deserialize<List<User>>(json);
        }

        public async Task DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7124/api/employee/{id}");
            response.EnsureSuccessStatusCode();

        }
        
        public async Task UpdateAccess(User user, string newDirection, string location)
        {
            AuthorizateUser auth = new AuthorizateUser(user.Id, user.Username, user.Password, user.Card, user.Pin,DateTime.UtcNow, location, newDirection );
            var json = JsonSerializer.Serialize(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(await content.ReadAsStringAsync());
            var response = await _httpClient.PostAsync($"https://localhost:7124/api/access/add", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateUser(User user)
        {
            
            user.Employee.Accesses = null;
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            int id = user.Id;
            Console.WriteLine(await content.ReadAsStringAsync());
            var response = await _httpClient.PutAsync($"https://localhost:7124/api/employee/{id}", content);
            response.EnsureSuccessStatusCode();

        }

        public async Task AddUser(User user)
        {

            user.Employee.Accesses = null;
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"https://localhost:7124/api/employee/add", content);
            Console.WriteLine(await content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();

        }
    }
}
