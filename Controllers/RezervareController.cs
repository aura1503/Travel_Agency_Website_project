using Microsoft.AspNetCore.Mvc;
using AgentieTurism.Data;
using AgentieTurism.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgentieTurism.Controllers
{
    public class RezervareController : Controller
    {
        private readonly TurismDbContext _context;

        public RezervareController(TurismDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int idOferta)
        {
            if (HttpContext.Session.GetString("Rol") != "client")
                return RedirectToAction("Index", "Home");

            var rezervare = new Rezervare
            {
                IdOferta = idOferta,
                DataRezervare = DateTime.Now
            };

            return View(rezervare);
        }

        [HttpGet]
        public IActionResult ListaClient()
        {
            var idClient = HttpContext.Session.GetInt32("IdClient");
            if (idClient == null || HttpContext.Session.GetString("Rol") != "client")
                return RedirectToAction("Index", "Home");

            var rezervari = _context.Rezervari
                .Include(r => r.Oferta)
                .ThenInclude(o => o.Hotel)
                .Where(r => r.IdClient == idClient)
                .ToList();

            return View(rezervari);
        }

        [HttpPost]
        public IActionResult Create(Rezervare rezervare)
        {
            var idClient = HttpContext.Session.GetInt32("IdClient");

            if (idClient == null || HttpContext.Session.GetString("Rol") != "client")
                return RedirectToAction("Index", "Home");

            rezervare.IdClient = idClient.Value;
            rezervare.DataRezervare = DateTime.Now; 

            _context.Rezervari.Add(rezervare);
            _context.SaveChanges();

            return RedirectToAction("ListaClient");
        }
        [HttpGet]
        public IActionResult ListaHotel()
        {
            var idHotel = HttpContext.Session.GetInt32("IdHotel");
            if (idHotel == null || HttpContext.Session.GetString("Rol") != "hotel")
                return RedirectToAction("Index", "Home");

            var rezervari = _context.Rezervari
                .Include(r => r.Client)
                .Include(r => r.Oferta)
                .ThenInclude(o => o.Hotel)
                .Where(r => r.Oferta.IdHotel == idHotel)
                .ToList();

            return View(rezervari);
        }

        [HttpPost]
        public IActionResult Sterge(int id)
        {
            var rezervare = _context.Rezervari.Find(id);

            var idClient = HttpContext.Session.GetInt32("IdClient");
            if (rezervare == null || rezervare.IdClient != idClient)
                return NotFound();

            _context.Rezervari.Remove(rezervare);
            _context.SaveChanges();

            return RedirectToAction("ListaClient");
        }

    }

}