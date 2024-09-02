using Microsoft.EntityFrameworkCore;
using paw_examen.Server.Models;


namespace ExamPrep.Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Protest> Proteste {  get; set; }
        public DbSet<Locatie> Locatii {  get; set; }

        public ApplicationDbContext() : base()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Protest>()
                .HasOne(c => c.Locatie)
                .WithMany()
                .HasForeignKey(c => c.LocatieId);

            modelBuilder.Entity<Locatie>().HasData(
                new Locatie { Id = 1, Judet = "Bucuresti", Oras = "Bucuresti", Numar_locuitori = 10000 },
                new Locatie { Id = 2, Judet = "Ilfov", Oras = "Buftea", Numar_locuitori = 2500 },
                new Locatie { Id = 3, Judet = "Prahova", Oras = "Ploiesti", Numar_locuitori = 7000 }
            );

            modelBuilder.Entity<Protest>().HasData(
                new Protest { Id = 1, Denumire = "Protest 1", Descriere = "Protest despre problema 1", Data = new DateTime(2022, 2, 13), Numar_participanti = 9000, LocatieId = 1 },
                new Protest { Id = 2, Denumire = "Protest 2", Descriere = "Protest despre problema 2", Data = new DateTime(2022, 3, 23), Numar_participanti = 3700, LocatieId = 1 },
                new Protest { Id = 3, Denumire = "Protest 3", Descriere = "Protest despre problema 3", Data = new DateTime(2022, 1, 17), Numar_participanti = 7000, LocatieId = 1 },
                new Protest { Id = 4, Denumire = "Protest 4", Descriere = "Protest despre problema 4", Data = new DateTime(2022, 6, 3), Numar_participanti = 500, LocatieId = 3 },
                new Protest { Id = 5, Denumire = "Protest 5", Descriere = "Protest despre problema 5", Data = new DateTime(2022, 5, 5), Numar_participanti = 1000, LocatieId = 2 }
            );
        }
    }

}
