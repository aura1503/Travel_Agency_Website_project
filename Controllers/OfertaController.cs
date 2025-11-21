using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgentieTurism.Data;
using AgentieTurism.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AgentieTurism.Controllers
{
    public class OfertaController : Controller
    {
        private readonly TurismDbContext _context;

        public OfertaController(TurismDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Detalii(int id)
        {
            var oferta = _context.Oferte
                .Include(o => o.Hotel)
                .FirstOrDefault(o => o.IdOferta == id);

            if (oferta == null)
                return NotFound();

            return View(oferta);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != "hotel")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Oferta oferta, IFormFile imagine)
        {
            if (HttpContext.Session.GetString("Rol") != "hotel")
                return RedirectToAction("Index", "Home");

            if (imagine != null && imagine.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagini");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagine.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagine.CopyTo(stream);
                }

                oferta.NumeImagine = fileName;
            }

            oferta.IdHotel = HttpContext.Session.GetInt32("IdHotel") ?? 0;
            _context.Oferte.Add(oferta);
            _context.SaveChanges();

            return RedirectToAction("HotelHome", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}