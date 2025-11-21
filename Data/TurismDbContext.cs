using Microsoft.EntityFrameworkCore;
using AgentieTurism.Models;

namespace AgentieTurism.Data
{
    public class TurismDbContext : DbContext
    {
        public TurismDbContext(DbContextOptions<TurismDbContext> options) : base(options) { }

        public DbSet<Hotel> Hoteluri { get; set; }
        public DbSet<Oferta> Oferte { get; set; }
        public DbSet<Client> Clienti { get; set; }
        public DbSet<Rezervare> Rezervari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasKey(h => h.IdHotel);
            modelBuilder.Entity<Oferta>().HasKey(o => o.IdOferta);
            modelBuilder.Entity<Client>().HasKey(c => c.IdClient);
            modelBuilder.Entity<Rezervare>().HasKey(r => r.IdRezervare);

            modelBuilder.Entity<Oferta>()
                .HasOne(o => o.Hotel)
                .WithMany(h => h.Oferte)
                .HasForeignKey(o => o.IdHotel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervare>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Rezervari)
                .HasForeignKey(r => r.IdClient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervare>()
                .HasOne(r => r.Oferta)
                .WithMany(o => o.Rezervari)
                .HasForeignKey(r => r.IdOferta)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
