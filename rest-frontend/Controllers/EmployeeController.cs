using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using rest_frontend.Models;
using rest_frontend.Services;

/*!
 * A frontend vezérlésének névtere
 */
namespace rest_frontend.Controllers
{
    /*!
     * A frontend munkavállalókat kezelő része
     */

    /*!
     * Az EmployeeController osztály definiálása.
     */
   public class EmployeeController : Controller
    {
        /*!
         * Csak olvasható mező deklarálása
         * értékét konstruktorban lehet beállítani.
         */
        private readonly EmployeeService _employeeService;
        /*!
         * Konstruktor inicializálja a mezőt.
         */
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /*!
         * A ki és beléptetést kezeljük egy Post kérés nyomán
         */
        [HttpPost("movement")]
        
        /*!
         * Metódus paramétereket fogad a mozgás rögzítéséhez,
         * amelyeket a HTTP kérés űrlap adataiból (FromForm) vesz át.
         */
        public async Task<IActionResult> Movement([FromForm]int id, [FromForm] string direction, [FromForm] string location)
        {
            /*!
             * Kezeljük, hogy a belépett dolgozó még egyszer ne lépehessen be, vagy a kilépett ne léphessen ki
             */
            if (direction != "In" && direction != "Out") return BadRequest("Wrong direction value");
            /*!
             * Lekérünk egy dolgozót a user_id alapján, ha nem létezik hibát dobunk
             */
            User user = await _employeeService.GetSingleUser(id);
            if ( user == null) return NotFound("Nincs ilyen dolgozó");
            
            try
            {
                /*!
                 * A formból bekért adatok alapján létrehozzuk a belépést
                 */
                await _employeeService.UpdateAccess(user, direction, location);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                /*!
                 * Nem 2xx HTTP státuszkód esetén elutasítjuk a belépést
                 */
                return BadRequest(ex.Message);
            }
        }

        /*!
         * Dolgozói adatok módosítása egy Post kérés nyomán
         */
        [HttpPost("update")]
        
        /*!
         * Metódus paramétereket fogad a módosításhoz,
         * amelyeket a HTTP kérés űrlap adataiból (FromForm) vesz át.
         */
        public async Task<IActionResult> Update([FromForm] int id, [FromForm] string pin,
            [FromForm] string password, [FromForm] string username,
            [FromForm] bool locationoffice, [FromForm] bool locationhq, [FromForm] string roles)
        {
            /*!
             * A formból kért id alapján megpróbáljuk példányosítani a usert
             */
            User user = await _employeeService.GetSingleUser(id);
            if (user == null) return NotFound("Nincs ilyen dolgozó");

            /*!
             * A lekért user engedélyezett helyszíneinek a listáját töröljük, majd újra töltjük a checkboxban kijelöltekkel
             */
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
            /*!
             * A többi bekért adatot hozzárendeljük a dolgozó példányhoz
             */
            user.Username = username;
            user.Password = password;
            user.Pin = pin;
            user.Roles[0] = new Role(roles);
            
            /*!
             * Elküldjük a PUT kérést a szervernek
             * -Ha rendben van minden aktualizál
             * -Hibát dob, ha nincs rendben minden
             */
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

        /*!
         * A formból kapott id alapján kérjük a dolgozó törlését a szervertől
         * -Ha rendben van minden aktualizál
         * -Hibát dob, ha nincs rendben minden
         */
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
        
        /*!
         * A formban bekért adatok alapján létrehozunk egy user példányt és elküldjük a Post kérést az API-nak
         */
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromForm] string firstname, [FromForm] string lastname,
            [FromForm] DateTime bdate, [FromForm] string password, [FromForm] string pin, [FromForm] string role,
            [FromForm] bool locationoffice, [FromForm] bool locationhq, [FromForm] string validation )
        {
            /*!
            * A helyszínek listája (dolgozó - vezető)
            */
            List<Location> locations = new List<Location>();

            if (locationhq) {
                locations.Add(new Location("HQ"));
            }
            if (locationoffice)
            {
                locations.Add(new Location("Office"));
            }
            
            /*!
             * Ellenőrzés
             */ 
            List<ValidationTypes> validationTypes = new List<ValidationTypes>();
            validationTypes.Add(new ValidationTypes(validation));

            /*!
             * Véletlen kártyaszám hozzárendelése a szabályokhoz
             */
            List<Role> roles = new List<Role>();
            roles.Add(new Role(role));
            Random random = new Random();
            int rnd = random.Next(8, 17);
            string cardNumber = "";
            for (int i = 0; i < rnd; i++)
            {
                cardNumber += random.Next(0, 10);
            }
            /*!
             * Új felhasználó adatai az adatbázisban való tároláshoz
             */ 
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

             /*!
             * A kéréshez Json formátumra konvertáljuk a user példányt
             */
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _employeeService.AddUser(user);
            return RedirectToAction("Index");
        }
        
        /*!
         * Összes felhasználó listája
         */
        public async Task<IActionResult> Index()
        {
            List<User> users = await _employeeService.GetAllEmployeesAsync();
            return View(users);
        }

        /*!
         * Ha egy kifejezett user adatait akarjuk megnézni, lekérjük a GetSingleUser metódussal az adatait, és
         * átírányítunk a user oldalra.
         */
        public async Task<IActionResult> User(int id)
        {            
            User user = await _employeeService.GetSingleUser(id);
            return View(user);
        }
    }
}
