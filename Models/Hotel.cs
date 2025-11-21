namespace AgentieTurism.Models
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string NumeHotel { get; set; }
        public string NumeAdmin { get; set; }
        public int Capacitate { get; set; }
        public string Oras { get; set; }
        public string Adresa { get; set; }
        public string NrTelefon { get; set; }
        public string Parola { get; set; }

        public ICollection<Oferta> Oferte { get; set; }
    }
}
