using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    /// <summary>
    /// 2. feladat: Sportolo osztály létrehozása 
    /// Egy sportoló adatait képezi le a kalapacsvetes.txt fájl adatai szerint
    /// </summary>
    public class SportoloNezet
    {
        public decimal Eredmeny { get; set; }
        public string SportoloNev { get; set; } = string.Empty; // Megadható a kezdőérték, ha nem kapna a konstruktoról
        public string OrszagKod { get; set; } = string.Empty;
        public string Helyszin { get; set; } = string.Empty;
        public DateOnly Datum { get; set; }
        public int Helyezes { get; set; }

        /// <summary>
        ///     A Sportolo osztály konstruktora, lefut minden esetben
        ///  amikor létrehozunk (példányosítunk) egy új sportolót
        /// </summary>
        /// <param name="versenyAdatok">Kalapacsvetes.txt egy sorát tartalmazza </param>
        ///                             1;86,74;Yuriy Sedykh;URS;Stuttgart;1986.08.30
        /// <exception cref="ArgumentException"> Hibát jelez, ha nem megfelelő az elemek száma </exception>
        /// <exception cref="FormatException"> Hibát jelez, ha nem hajtható végre az átalakítás </exception>
        public SportoloNezet(List<string> versenyAdatok) // Konstruktor, amelyinicializálja az adattagjait
        {
            if (versenyAdatok.Count != 6)
                throw new ArgumentException("A bemeneti lista pontosan 6 elemet kell tartalmazzon.");

            // Alapértelmezetten a decimal.TryParse egész számot csinál a vesszővel elválasztott tizedes számokból, ha hiányzik aCultureInfo
            if (!decimal.TryParse(versenyAdatok[1], NumberStyles.Number, new CultureInfo("hu-HU"), out decimal eredmeny))
                throw new FormatException("Eredmény nem érvényes tizedes szám.");
            if (!DateOnly.TryParse(versenyAdatok[5], out DateOnly datum))
                throw new FormatException("Dátum formátuma érvénytelen.");

            // Ha eddig eljutottunk, minden érvényes
            Helyezes = int.Parse(versenyAdatok[0]);
            Eredmeny = eredmeny;
            SportoloNev = versenyAdatok[2];
            OrszagKod = versenyAdatok[3];
            Helyszin = versenyAdatok[4];
            Datum = datum;
        }
    }
}
