using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class ApplicationDbContext : DbContext
    {
        /*** A táblák meghatározása ***/

        //       C#osztály     Tábla neve   
        public DbSet<Helyszin> Helyszin { get; set; }
        public DbSet<Orszag> Orszag { get; set; }
        public DbSet<Sportolo> Sportolo { get; set; }
        public DbSet<Verseny> Verseny { get; set; }
        public DbSet<Erdeklodes> Erdeklodes { get; set; }


        //Alapértelmezett konstruktor - könnyebben megtalálja a dbContext osztályt az EF
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                                        // Ne szerepeljen két helyen a konnekció beállítás
                optionsBuilder.UseSqlServer(DapperAdatbazisKapcsolat.ConnectionString);
            }
        }

        
    }
}
