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

            //A megkapott From adatok alapj�n megh�vjuk a HomeService Login met�dus�t
            int userId = await _homeService.Login(userdata, password);
            User user = new User();
            
            if (userId>=1)
            { 
                //ha sikeres volt a k�r�s �s l�tezik ilyen dolgoz�, bel�p�nk az Employee/Index oldalra
                user = await _employeeService.GetSingleUser(userId);
                HttpContext.Session.SetString("user_id", userId.ToString());
                HttpContext.Session.SetString("fail", "false");
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                //ha nem volt sikeres a k�r�s visszal�p�nk a Home/Index oldalra
                //(login oldal �s Session alapj� ki�rjuk, hogy hib�s adatokat kaptunk)
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
