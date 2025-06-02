using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozosKonyvtar.MODEL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kalapacsvetes.SERVICE
{
    public class SportolokService
    {
        /// <summary>
        ///      Egy sportoló adatokat tartalmazó szöveges lista elemeit
        ///  alakítja át sportolókat tartalmazó listává. 
        /// </summary>
        /// <param name="adatLista">Sportolo adatokat tartalmazo string[] tartalmú lista</param>
        /// <returns>Sportolo elemeket tartalmazó lista</returns>
        public static List<SportoloNezet> SportolokbaOlvas(List<string[]> adatLista)
        {
            List<SportoloNezet> sportolok = new List<SportoloNezet>();

            foreach (var item in adatLista)
            {
                // A feladat szerint a konstruktorban kell létrehozni a sportolót
                SportoloNezet ujSportolo = new SportoloNezet(item.ToList()); 
                sportolok.Add(ujSportolo);
            }

            return sportolok;
        }

        /***   Megszámlálás tétel *****************************************************************************************************************************/
        /// <summary>
        ///  Megszámlálás tétele
        /// </summary>
        /// <param name="lista">amelynek az elemeit megszámoljuk</param>
        /// <returns>Elemek összege</returns>
        public static int Megszamlalalas(List<SportoloNezet> lista)
        {
            int darab = 0;
            foreach (var item in lista)
            {
                darab++; // Minden elem egy dobás
            }

            return darab;
        }

        /** Összegzés tétel **********************************************************************************************************************************/
        /// <summary>
        ///  Magyar sportololók összesített átlageredménye Linq-val
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static decimal MagyarEredmenyAtlagLinq(List<SportoloNezet> sportolok)
        {
            decimal atlag = 0;

            // Örszágkód alapján kikeressük a magyar sportolókat
            var magyarSportolok = sportolok
                .Where(s => s.OrszagKod == "HUN") // Kitesszük új listába országkód alapján a magyarokat
                .ToList();

            // Ha van találat kiszámítjuk az átlagot
            if (magyarSportolok.Any())
            {
                // Kerekítés                  Eredmeny átlaga      tizedesek
                atlag = Math.Round(magyarSportolok.Average(s => s.Eredmeny), 2);
                //Console.WriteLine($"A magyar sportolók átlageredménye: {atlag}");
            }
            return atlag;
        }

        /// <summary>
        ///  Összegzés ciklussal
        /// </summary>
        /// <param name="sportolok">A kalapacsvetes.txt-ből gyüjtött adatok</param>
        /// <returns>A magyar sportolók dobás hosszának átlaga</returns>
        public static decimal MagyarEredmenyAtlagCiklus(List<SportoloNezet> sportolok)
        {
            // A metódus legelején felvesszük a változókat. Mindíg adj kezdő értéket!
            decimal atlag = 0;
            decimal osszeg = 0;
            int db = 0;

            foreach (var s in sportolok) // Vágignézzük soronként a lista telemeit
            {
                if (s.OrszagKod == "HUN") // Ha a sportoló magyar gyerek
                {
                    osszeg += s.Eredmeny;  // hozzáadjuk az eredményét az eredmények összegéhez
                    db++;                 // Átlagszámításhoz kell, hány magyar sportoló volt
                                          // (1-el emeljük a sportolók számát)
                }
            }

            if (db > 0)   // Ha volt a listában magyar sportoló kiszámoljuk az átlagot (Az átlagszámítás
            {                  //nem része az összegzés tételének
                atlag = osszeg / db;
            }
            return Math.Round(atlag, 2);
        }

        /** Kiválogatás tétel **********************************************************************************************************************************/
        /// <summary>
        ///  Kiválogatás Linq-val
        /// </summary>
        /// <param name="sportolok">A kalapacsvetes.txt-ből gyüjtött adatok</param>
        /// <param name="ev">A kiválogatás éve</param>
        /// <returns>Az adott évben bekerült sportolók</returns>
        public static List<SportoloNezet> LegjobbEredmenyekLinq(List<SportoloNezet> sportolok, int ev)
        {
            List<SportoloNezet> legjobbSportolok = new List<SportoloNezet>();
            var eredmenyek = sportolok.Where(d => d.Datum.Year == ev) // Kiválogatjuk az év dobásait új listába
                                      .ToList();

            if (eredmenyek.Count == 0) // Ha nem volt verseny abban az évben
            {
                Console.WriteLine("Az adott évben nem került be egy dobás eredménye sem a legjobbak közé.");
            }
            else // volt verseny abban az évben
            {
                foreach (var elem in eredmenyek.OrderByDescending(s => s.Eredmeny)) // rendezzük eredmény szerint
                {
                    legjobbSportolok.Add(elem); // betesszük az év sportolói közé
                }
            }

            return legjobbSportolok;
        }

        /// <summary>
        ///  Kiválogatás egyetlen Linq-val
        /// </summary>
        /// <param name="sportolok"></param>
        /// /// <param name="ev">A kiválogatás éve</param>
        /// <returns>az év sportolói eredmény szerint csökkenő sorrendben</returns>
        public static List<SportoloNezet> LegjobbEredmenyek1Linq(List<SportoloNezet> sportolok, int ev)
        {
            return sportolok.Where(s => s.Datum.Year == ev)
                            .OrderByDescending(s => s.Eredmeny)
                            .ToList();
        }

        /// <summary>
        /// Kiválogatás ciklussal
        /// </summary>
        /// <param name="sportolok"></param>
        /// <param name="ev">A kiválogatás éve</param>
        /// <returns></returns>

        public static List<SportoloNezet> LegjobbEredmenyekCiklus(List<SportoloNezet> sportolok, int ev)
        {
            List<SportoloNezet> eredmeny = new();
            foreach (var s in sportolok)
            {
                if (s.Datum.Year == ev)
                {
                    eredmeny.Add(s);
                }
            }
            return eredmeny.OrderByDescending(s => s.Eredmeny).ToList();
        }

        /*** Kiválogatás csoportosítva ************************************************************************************************************************/
        public static List<Statisztika> KivalogatasOrszagSzerintLinq(List<SportoloNezet> sportolok)
        {

            var stat = sportolok.GroupBy(s => s.OrszagKod)
                                .OrderByDescending(g => g.Count()) // leggyakoribb előre
                                .ThenBy(g => g.Key)               // azon belül ABC szerint
                                .Select(g => new Statisztika
                                {
                                    Orszagkod = g.Key,
                                    DobasSzam = g.Count()
                                });

            return stat.ToList();
        }

        public static List<Statisztika> KivalogatasOrszagSzerintCiklus(List<SportoloNezet> sportolok)
        {
            List<string> orszagok = new();

            // 1. lépés: országkódok kigyűjtése, ismétlés nélkül
            foreach (var s in sportolok)
            {
                if (!orszagok.Contains(s.OrszagKod))
                {
                    orszagok.Add(s.OrszagKod);
                }
            }

            List<Statisztika> statisztikak = new();

            // 2. lépés: minden országhoz megszámláljuk a dobásokat
            foreach (var orszag in orszagok)
            {
                int db = 0;

                foreach (var s in sportolok)
                {
                    if (s.OrszagKod == orszag)
                    {
                        db++;
                    }
                }

                statisztikak.Add(new Statisztika
                {
                    Orszagkod = orszag,
                    DobasSzam = db
                });
            }
            return statisztikak;
        }

        /// <summary>
        /// A beolvasott adatlistából soronként minden táblában létrehoz egy rekordot (ha még nincs)
        /// Feltöltünk egy - egy ... tabla szerkezetű osztályt
        /// </summary>
        /// <param name="context">Adatbázis elérés</param>
        /// <param name="verseny">A beolvasott adatlista</param>
        public static void IrdAdatbazisba(ApplicationDbContext context, List<SportoloNezet> verseny)
        {
            Console.WriteLine();
            Console.WriteLine("Adatok betöltése az adatbázisba...");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"{"Név",-25} {"Ország",-8} {"Helyszín",-15} {"Dátum",10} {"Eredmény",10}");
            Console.WriteLine("------------------------------------------------------------");


            foreach (var item in verseny) // tehát soronként bejárjuk az adatlistát
                {
                Console.WriteLine($"{item.SportoloNev,-25} {item.OrszagKod,-8} {item.Helyszin,-15} {item.Datum,10} {item.Eredmeny,10}");

                // Helyszin Ha nincs még az adatbázisban beírjuk
                if (!context.Helyszin.Any(h => h.HelyszinNev == item.Helyszin))
                    {
                        context.Helyszin.Add(new Helyszin { HelyszinNev = item.Helyszin });
                        context.SaveChanges();
                    }

                    // Ország
                    if (!context.Orszag.Any(o => o.OrszagKod == item.OrszagKod))
                    {
                        context.Orszag.Add(new Orszag { OrszagKod = item.OrszagKod });
                        context.SaveChanges();
                    }

                    // Sportoló
                    // Ki kell keressük, mi az OrszágId (OrszágKód már adatbázisban)
                    Orszag orszagAdatbazisban = context.Orszag.FirstOrDefault(orszag => orszag.OrszagKod == item.OrszagKod);
                    if (!context.Sportolo.Any(s => s.SportoloNev == item.SportoloNev)) // Ugyanolyan névvel másik országból??? Nem valószínű
                    {
                        context.Sportolo.Add(new Sportolo
                        {
                            SportoloNev = item.SportoloNev,
                            OrszagId = orszagAdatbazisban.OrszagId
                        });
                        context.SaveChanges();
                    }

                    // Verseny
                    var helyszinAdatbazisban = context.Helyszin.FirstOrDefault(h => h.HelyszinNev == item.Helyszin);
                    var sportoloAdatbazisban = context.Sportolo.FirstOrDefault(h => h.SportoloNev == item.SportoloNev);
                    //Gondold meg! Itt minden rekord csak egyedi lehet, nem kell létezőséget ellenőrizni

                    context.Verseny.Add(new Verseny
                    {
                        Datum = item.Datum,
                        HelyszinId = helyszinAdatbazisban.HelyszinId,
                        SportoloId = sportoloAdatbazisban.SportoloId,
                        Helyezes = item.Helyezes,
                        Eredmeny = item.Eredmeny
                    });
                    context.SaveChanges();
                
            }

        }
    }

}
