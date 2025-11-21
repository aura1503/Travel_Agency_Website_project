using Microsoft.AspNetCore.Mvc;
using AgentieTurism.Data;
using AgentieTurism.Models;
using MySql.Data.MySqlClient;

namespace AgentieTurism.Controllers
{
    public class ClientController : Controller
    {
        private readonly TurismDbContext _context;

        public ClientController(TurismDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Client client, string ConfirmareParola)
        {
            if (client.Parola != ConfirmareParola)
            {
                ModelState.AddModelError("ConfirmareParola", "Parolele nu coincid.");
                return View(client);
            }

            try
            {
                _context.Clienti.Add(client);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("IdClient", client.IdClient);
                HttpContext.Session.SetString("Rol", "client");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Eroare la salvarea în baza de date: " + ex.Message);
            }
            return RedirectToAction("ClientHome", "Home");
        }
    }
}