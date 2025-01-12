using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using rest_frontend.Models;
using rest_frontend.Services;

namespace rest_frontend.Controllers
{

   public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("movement")]
        public async Task<IActionResult> Movement([FromForm]int id, [FromForm] string direction, [FromForm] string location)
        {
            if (direction != "In" && direction != "Out") return BadRequest("Not existing direction value");
            User user = await _employeeService.GetSingleUser(id);

            if ( user == null) return NotFound("Nincs ilyen dolgozó");
            
            try
            {
                await _employeeService.UpdateAccess(user, direction, location);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] int id, [FromForm] string pin,
            [FromForm] string password, [FromForm] string username,
            [FromForm] bool locationoffice, [FromForm] bool locationhq, [FromForm] string roles)
        {
            User user = await _employeeService.GetSingleUser(id);
            if (user == null) return NotFound("Nincs ilyen dolgozó");

            List<Location> locations = new List<Location>();
            Location hq = new Location("HQ");
            Location office = new Location("Office");
            user.Employee.AuthorizedLocations = null;

            if (locationoffice) 
            {
                locations.Add(office);
            }
            

            if (locationhq)
            {
                locations.Add(hq);
            }
            
            user.Employee.AuthorizedLocations= locations;
            user.Username = username;
            user.Password = password;
            user.Pin = pin;
            user.Roles[0] = new Role(roles);
            
            try
            {
                await _employeeService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromForm] string firstname, [FromForm] string lastname,
            [FromForm] DateTime bdate, [FromForm] string password, [FromForm] string pin, [FromForm] string role,
            [FromForm] bool locationoffice, [FromForm] bool locationhq, [FromForm] string validation )
        {
            List<Location> locations = new List<Location>();

            if (locationhq) {
                locations.Add(new Location("HQ"));
            }
            if (locationoffice)
            {
                locations.Add(new Location("Office"));
            }
            
            List<ValidationTypes> validationTypes = new List<ValidationTypes>();
            validationTypes.Add(new ValidationTypes(validation));

            List<Role> roles = new List<Role>();
            roles.Add(new Role(role));
            Random random = new Random();
            int rnd = random.Next(8, 17);
            string cardNumber = "";
            for (int i = 0; i < rnd; i++)
            {
                cardNumber += random.Next(0, 10);
            }
            User user = new User();
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Username = "";
            user.Password = password;
            user.Pin = pin;
            user.Card= cardNumber;
            user.BirthDate = bdate;
            user.Employee=new Employee(true, DateTime.UtcNow, locations);
            user.Employee.Accesses = null;
            user.ValidationTypes = validationTypes;
            user.Roles = roles;

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            await _employeeService.AddUser(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await _employeeService.GetAllEmployeesAsync();
            return View(users);
        }

        public async Task<IActionResult> User(int id)
        {
            User user = await _employeeService.GetSingleUser(id);
            return View(user);
        }
    }
}
