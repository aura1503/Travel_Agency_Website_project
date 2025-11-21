using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgentieTurism.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AgentieTurism.Controllers
{
    public class LoginController : Controller
    {
        private readonly TurismDbContext _context;

        public LoginController(TurismDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Index(string tipUtilizator, string identificator, string parola)
        {
            if (tipUtilizator == "client")
            {
                var client = _context.Clienti.FirstOrDefault(c => c.Email == identificator && c.Parola == parola);
                if (client != null)
                {
                    HttpContext.Session.SetInt32("IdClient", client.IdClient);
                    HttpContext.Session.SetString("Rol", "client");
                    return RedirectToAction("ClientHome", "Home");
                }
            }
            else if (tipUtilizator == "hotel")
            {
                if (int.TryParse(identificator, out int idHotel))
                {
                    var hotel = _context.Hoteluri.FirstOrDefault(h => h.IdHotel == idHotel && h.Parola == parola);
                    if (hotel != null)
                    {
                        HttpContext.Session.SetInt32("IdHotel", hotel.IdHotel);
                        HttpContext.Session.SetString("Rol", "hotel");
                        return RedirectToAction("HotelHome", "Home");
                    }
                }
            }

            ViewBag.Eroare = "Date de autentificare incorecte.";
            return View();
        }
    }
}