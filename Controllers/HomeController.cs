using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using AgentieTurism.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly TurismDbContext _context;

        public HomeController(ILogger<HomeController> logger, TurismDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string oras, int? capacitate, DateTime? dataStart, DateTime? dataFinal)
        {
            var oferte = _context.Oferte
                .Include(o => o.Hotel)
                .AsQueryable();

            if (!string.IsNullOrEmpty(oras))
            {
                oferte = oferte.Where(o => o.Hotel.Oras.Contains(oras));
            }

            if (capacitate.HasValue)
            {
                oferte = oferte.Where(o => o.Hotel.Capacitate >= capacitate.Value);
            }

            if (dataStart.HasValue)
            {
                oferte = oferte.Where(o => o.DataStart <= dataStart.Value);
            }

            if (dataFinal.HasValue)
            {
                oferte = oferte.Where(o => o.DataFinal >= dataFinal.Value);
            }

            return View(oferte.ToList());
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Detalii(int id)
        {
            var oferta = _context.Oferte
                .Include(o => o.Hotel)
                .FirstOrDefault(o => o.IdOferta == id);

            if (oferta == null)
                return NotFound();

            return View(oferta);
        }

        public IActionResult ClientHome(string oras, int? capacitate, DateTime? dataStart, DateTime? dataFinal)
        {
            var oferte = _context.Oferte
                .Include(o => o.Hotel)
                .AsQueryable();

            if (!string.IsNullOrEmpty(oras))
                oferte = oferte.Where(o => o.Hotel.Oras.Contains(oras));

            if (capacitate.HasValue)
                oferte = oferte.Where(o => o.Hotel.Capacitate >= capacitate.Value);

            if (dataStart.HasValue)
                oferte = oferte.Where(o => o.DataStart <= dataStart.Value);

            if (dataFinal.HasValue)
                oferte = oferte.Where(o => o.DataFinal >= dataFinal.Value);

            return View("ClientHome", oferte.ToList());
        }

        public IActionResult HotelHome()
        {
            var idHotel = HttpContext.Session.GetInt32("IdHotel");
            if (idHotel == null || HttpContext.Session.GetString("Rol") != "hotel")
                return RedirectToAction("Index");

            var oferte = _context.Oferte
                .Include(o => o.Hotel)
                .Where(o => o.IdHotel == idHotel)
                .ToList();

            return View("HotelHome", oferte);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}