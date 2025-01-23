using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rest_frontend.Models;
using rest_frontend.Services;
/*!
 * A frontend vezérlésének névtere
 */
namespace rest_frontend.Controllers
{
    /*!
     * A frontend bejelentkezési műveleteket kezelő része.
     */

    /*!
     * A HomeController osztály definiálása.
     */
    public class HomeController : Controller
    {
        /*!
         * Csak olvasható mezők deklarálása
         * értéküket konstruktorokban lehet beállítani.
         */
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        private readonly EmployeeService _employeeService;

        /*!
         * A konstruktor inicializálja a mezőket.
         */
        public HomeController(ILogger<HomeController> logger, HomeService homeservice, EmployeeService employeeService)
        {
            _logger = logger;
            _homeService = homeservice;
            _employeeService = employeeService;
        }

         /*!
         * A beléptetést kezeljük egy Post kérés nyomán
         */
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userdata, [FromForm] string password)
        {

            /*!
             * A megkapott form adatok alapján meghívjuk a HomeService Login metódusát.
             */
            int userId = await _homeService.Login(userdata, password);
            User user = new User();
            
            /*!
             * Ha sikeres volt a kérés és létezik ilyen dolgozó, belépünk az Employee/Index oldalra.
             */
            if (userId>=1)
            {                 
                user = await _employeeService.GetSingleUser(userId);
                HttpContext.Session.SetString("user_id", userId.ToString());
                HttpContext.Session.SetString("fail", "false");
                return RedirectToAction("Index", "Employee");
            }
            /*!
             * Ha nem volt sikeres a kérés visszalépünk a Home/Index oldalra
             * (login oldal) és Session alapján kiírjuk, hogy hibás adatokat kaptunk)
             */
            else
            {                
                HttpContext.Session.SetString("fail","true");
                return View("Index");
            }
            
            
        }

        /*!
         * Átirányítás a login nézetre.
         */
        public IActionResult Index()
        {
          
            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*!
         * Átirányítás hibakezelő nézetre.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] /*!< A hibaválasz nincs gyorsítótárazva */
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
