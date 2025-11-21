namespace AgentieTurism.Models
{
    public class Client
    {
        public int IdClient { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string NrTelefon { get; set; }

        public ICollection<Rezervare> Rezervari { get; set; }
    }
}
