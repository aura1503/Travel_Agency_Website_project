using Microsoft.AspNetCore.Mvc;
using AgentieTurism.Data;
using AgentieTurism.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgentieTurism.Controllers
{
    public class HotelController : Controller
    {
        private readonly TurismDbContext _context;

        public HotelController(TurismDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Hotel hotel)
        {
            try
            {
                _context.Hoteluri.Add(hotel);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("IdHotel", hotel.IdHotel);
                HttpContext.Session.SetString("Rol", "hotel");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Eroare la salvarea în baza de date: " + ex.Message);
            }

            return RedirectToAction("HotelHome", "Home");
        }


    }
}
