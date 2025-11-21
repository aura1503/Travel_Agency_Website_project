namespace AgentieTurism.Models
{
    public class Rezervare
    {
        public int IdRezervare { get; set; }
        public int IdClient { get; set; }
        public int IdOferta { get; set; }
        public DateTime DataRezervare { get; set; }

        public Client Client { get; set; }
        public Oferta Oferta { get; set; }
    }
}
