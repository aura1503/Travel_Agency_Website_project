using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgentieTurism.Models
{
    public class Oferta
    {
        public int IdOferta { get; set; }

        public int IdHotel { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Data de început este obligatorie")]
        public DateTime DataStart { get; set; }

        [Required(ErrorMessage = "Data finală este obligatorie")]
        public DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu")]
        [Range(1, 100000, ErrorMessage = "Prețul trebuie să fie pozitiv")]
        public decimal Pret { get; set; }

        public string? NumeImagine { get; set; }

        public Hotel Hotel { get; set; }
        public ICollection<Rezervare> Rezervari { get; set; }
    }
}

