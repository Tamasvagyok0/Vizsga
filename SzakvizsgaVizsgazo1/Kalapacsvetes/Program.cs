using Kalapacsvetes.SERVICE;
using KozosKonyvtar;
using KozosKonyvtar.MODEL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Text;

namespace Kalapacsvetes
{
    // 1. feladat: Konzolos alkalmazás elkészítése
    public class Program
    {
        static void Main(string[] args)
        {
            // Szükséges paraméterek
            string fileNeve = "kalapacsvetes.txt";
            char elvalasztoKarakter = ';';
            bool vanFejlec = true;

            int ev = 0;

            // 3.feladat Beolvasás
            List<string[]> list = FajlKezeles.OlvasFajlbol(fileNeve, elvalasztoKarakter, vanFejlec);
            List<SportoloNezet> sportolok = SportolokService.SportolokbaOlvas(list);


            // 4. feladat megszámlálás tétel (kiírás pozíció helyettesítéssel)
            int elemekSzama = 0;
            // Lista tulajdonságait felhasználva , Linq-val
            elemekSzama = sportolok.Count();
            // C# ciklusban
            elemekSzama = SportolokService.Megszamlalalas(sportolok); 
            // A {0} helyörző, ide kerül a sztring után felsorolt változók 1 tagjának értéke
            Console.WriteLine("4. feladat: {0} dobás eredménye található.", elemekSzama);

            // 5. feladat összegzés tétel (kiírás interpolárással (szövegbe beágyzott változó értéke) ma ez az elfogadottabb)
            //                                                  interpolárás = beágyazás
            decimal eredmenyAtlag = SportolokService.MagyarEredmenyAtlagCiklus(sportolok);
            // a $ teszi lehetővé közvetlenül                     változó értékének beírását  F: Fixed-point (fixpontos formátum)
            //                                                                            (tizedes értékek száma előre rögzített)
            Console.WriteLine($"5. feladat: A magyar sportolók átlagosan {eredmenyAtlag:F2} métert dobtak. ");

            // 6. feladat kiválogatás tétel
            Console.WriteLine("6. feladat Adjon meg egy évszámot: ");
            ev = int.Parse(Console.ReadLine());
            Console.WriteLine();
            List<SportoloNezet> evSportoloi = SportolokService.LegjobbEredmenyekLinq(sportolok, ev);
            if (evSportoloi.Count > 0)
            {
                Console.WriteLine($"\t {evSportoloi.Count} db dobás került be ebben az évben.");
                foreach (var elem in evSportoloi)
                {
                    Console.WriteLine($"\t {elem.SportoloNev}");
                }
            }
            else
            {
                Console.WriteLine("\t Egyetlen dobás sem került be ebben az évben");
            }

            // 7. feladat Statisztika célú megszámlálás
            Console.WriteLine();
            Console.WriteLine("7.Feladat: ");
            List<Statisztika> statisztika = SportolokService.KivalogatasOrszagSzerintCiklus(sportolok);
            foreach (var orszag in statisztika)
            {
                Console.WriteLine($"\t {orszag.Orszagkod} - {orszag.DobasSzam,2} dobás");
            }

            // 8. Fájlba írás (Magyar sportolók)

            // Magyar sportolók kiszűrése
            var magyarok = sportolok.Where(s => s.OrszagKod == "HUN");

            // Sorok összeállítása
            var sorok = magyarok.Select(s =>
                $"{s.SportoloNev};{s.OrszagKod};{s.Helyszin};{s.Datum};{s.Eredmeny}"); 

            // Fájl kiírása UTF-8 kódolással
            File.WriteAllLines(@"C:\TEMP\magyarok.txt", sorok, Encoding.UTF8);

            /*** Adatok beírása adtbázisba  ***/
            ApplicationDbContext context = new ApplicationDbContext(); //Létrehozunk egy adatbázis konnekciót 
            
            using (context)
            {
                // Töröljük, majd újra létrehozzuk az adatbázist, hogy többször futtatható legyen a betöltés    
                Console.WriteLine("Adatbázis törlése...");
                context.Database.EnsureDeleted();
                Console.WriteLine();

                Console.WriteLine("Adatbázis felépítése...");
                context.Database.Migrate(); // Lefuttatja a migrations mappa alapján a létrehozást

                // Adatbázisba írás
                SportolokService.IrdAdatbazisba(context, sportolok);
                Console.WriteLine();

                // Vizsgázó2-be Dapperral
                Console.WriteLine("Adatbázis felépítése Dapperrel...");
                SportolokServiceDapper.IrdAdatbazisbaDapper(sportolok);
                Console.WriteLine();

                // SQL script genrálással
                Console.WriteLine("SQL script készítés");
                FajlKezeles.SqlScriptGenerator(sportolok);

            }


        }
    }
}
