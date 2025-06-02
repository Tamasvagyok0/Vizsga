using Dapper;
using KozosKonyvtar.MODEL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalapacsvetes.SERVICE
{
    public class SportolokServiceDapper
    {
        /* Konnekció az adatbázishoz  */

        // Nem kell DbContext, Helyette SqlConnectiont használunk.
        private const string ConnectionString = "Server=localhost;Database=Vizsgazo2;User Id=sa;" +
                                                "Password=1A2w3e4F;TrustServerCertificate=True;";

        public static void IrdAdatbazisbaDapper(List<SportoloNezet> versenyAdatok)
        {
            Console.WriteLine();
            Console.WriteLine("Adatok betöltése az adatbázisba...");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine($"{"Név",-25} {"Ország",-8} {"Helyszín",-15} {"Dátum",10} {"Eredmény",10}");
            Console.WriteLine("-----------------------------------------------------------------");

            // Tehát nem DbContext osztályt haználunk, hanem SqlConnectiont
            using var connection = new SqlConnection(DapperAdatbazisKapcsolat.ConnectionString); ; //Létre hozzuuk az SQlConnection osztályt  
            connection.Open(); // Csatlakozol az adatbázishoz
            // .NET 8 óta nem kell a using kulcsszó után {}

            //Mint az EF használata során végig iterálunk (az összes sort egymás után) a korábban beolvasott adatokon
            foreach (var item in versenyAdatok)
            {
                Console.WriteLine($"{item.SportoloNev,-25} {item.OrszagKod,-8} {item.Helyszin,-15} {item.Datum,10} {item.Eredmeny,10}");

                /* Orszag  */
                // meghatározzuk, milyen típust várunk eredményként
                var orszagId = connection.ExecuteScalar<int?>(
                    // A megismert SQL parancsokat használjuk paraméterrel      Látrehozzuk a kod paramétert (!!! SQL-ben kell elé a @ !!!)                     
                    "SELECT OrszagId FROM Orszag WHERE OrszagKod = @kod", new { kod = item.OrszagKod }); // Ellenőrizöd az adatbázisban
                if (orszagId == null) // Ha nincs adatbázisban, beszúrod (Nem kell SaveChanges() vagy hasonló eljárás a mentéshez)
                {              //nem várunk vissza adatot (sima Execute)
                    connection.Execute("INSERT INTO Orszag (OrszagKod) VALUES (@kod)", new { kod = item.OrszagKod });
                    // Ha eddig nem volt, mostmár biztosan van, kell majd a Verseny beszuráshoz
                    orszagId = connection.ExecuteScalar<int>("SELECT OrszagId FROM Orszag WHERE OrszagKod = @kod", new { kod = item.OrszagKod });
                }

                /* Helyszin  */
                var helyszinId = connection.ExecuteScalar<int?>(
                    "SELECT HelyszinId FROM Helyszin WHERE HelyszinNev = @nev", new { nev = item.Helyszin });
                if (helyszinId == null)
                {
                    connection.Execute("INSERT INTO Helyszin (HelyszinNev) VALUES (@nev)", new { nev = item.Helyszin });
                    helyszinId = connection.ExecuteScalar<int>("SELECT HelyszinId FROM Helyszin WHERE HelyszinNev = @nev", new { nev = item.Helyszin });
                }

                /* Sportolo  */
                var sportoloId = connection.ExecuteScalar<int?>(
                    "SELECT SportoloId FROM Sportolo WHERE SportoloNev = @nev", new { nev = item.SportoloNev });
                if (sportoloId == null)
                {
                    connection.Execute("INSERT INTO Sportolo (SportoloNev, OrszagId) VALUES (@nev, @orszagId)",
                        new { nev = item.SportoloNev, orszagId });
                    sportoloId = connection.ExecuteScalar<int>("SELECT SportoloId FROM Sportolo WHERE SportoloNev = @nev", new { nev = item.SportoloNev });
                }

                /* Verseny  */
                var datumDateTime = item.Datum.ToDateTime(TimeOnly.MinValue); // Sajnos a Dapper nem ismeri a DateOnly típust
                connection.Execute(
                    "INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes) VALUES (@datum, @helyszinId, @sportoloId, @eredmeny, @helyezes)",
                    new { datum = datumDateTime, helyszinId, sportoloId, eredmeny = item.Eredmeny, helyezes = item.Helyezes });
            }
        }
    }
}
