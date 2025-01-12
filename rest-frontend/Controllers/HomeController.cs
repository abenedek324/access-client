using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rest_frontend.Models;
using rest_frontend.Services;

namespace rest_frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        private readonly EmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, HomeService homeservice, EmployeeService employeeService)
        {
            _logger = logger;
            _homeService = homeservice;
            _employeeService = employeeService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userdata, [FromForm] string password)
        {

            
            int userId = await _homeService.Login(userdata, password);
            User user = new User();
            
            if (userId>=1)
            { 
                user = await _employeeService.GetSingleUser(userId);
                HttpContext.Session.SetString("user_id", userId.ToString());
                HttpContext.Session.SetString("fail", "false");
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                HttpContext.Session.SetString("fail","true");
                return View("Index");
            }
            
            
        }

        public IActionResult Index()
        {
          
            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
