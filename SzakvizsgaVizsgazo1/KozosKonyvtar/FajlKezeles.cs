using KozosKonyvtar.MODEL;
using KozosKonyvtar.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar
{
    public class  FajlKezeles
    {
        /// <summary>
        ///     Beolvassa egy listába az adatokat tartalmazó
        /// fájl sorait
        /// </summary>
        /// <param name="fajlNeve">Az adatokat tartalmazó fájl</param>
        /// <param name="elvalaszto">Az oszlopokat elválasztó karakter</param>
        /// <param name="vanFejlec">Az első sor eltér-e a többitől</param>
        ///                         Adatok helyett az oszlopok tartalmát (fejléc) jelzi
        /// <returns> Adatsorokat tartalmazó lista</returns>
        public static List<string[]> OlvasFajlbol(string fajlNeve, char elvalaszto, bool vanFejlec)
        {
            List<string[]> dataList = new List<string[]>();
            using (var reader = new StreamReader(fajlNeve, Encoding.UTF8)) // Kötelezően meghatározzuk, hogy UTF8 kódolással 
                                                                          // olvasson, ne próbálja meg kitalálni a fájl kódolását
            {
                // Fejléc átugrása
                if (vanFejlec)
                {
                    reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line != null)
                    {                                         // az üres elemeket ne dobja ki, hagyja meg a helyét
                        var columns = line.Split(elvalaszto, StringSplitOptions.None);
                        dataList.Add(columns);
                    }
                }
            }
            return dataList;
        }

        public static void SqlScriptGenerator(List<SportoloNezet> sportolok)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("-- SQL Script c# alkalmazásból --");

            /*** Adatbázis létrehozása ***/
            stringBuilder.AppendLine("USE master"); // Használatba vesszük az adatbázist
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine("CREATE DATABASE Vizsgazo4");
            stringBuilder.AppendLine("GO"); // Ha nincs go, nem jön létre az adatbázis
            stringBuilder.AppendLine("USE Vizsgazo4"); // Használatba vesszük az adatbázist
            stringBuilder.AppendLine("GO");
            
            stringBuilder.AppendLine(); // Üres sor

            /*** táblák létrehozása ***/
            //Orszagok
            stringBuilder.AppendLine("CREATE TABLE Orszag (");
            stringBuilder.AppendLine("OrszagId INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("OrszagKod NVARCHAR(10) NOT NULL");
            

            /*
             * Indexet hozunk létre a táblának. Index olyan állatfajta, amely megjegyzi a rekordok helyét. (Keresés gyorsítása)
             * A clustered egyedi, csak egy lehet egy táblán, ami az adott mező alapján fizikailag sorbarendezve 
             * tárolja az adatokat
             */            // megszorítás - kötöttség index név  elsődleges e- szerint rendezett              
            stringBuilder.AppendLine("CONSTRAINT PK_Orszag PRIMARY KEY CLUSTERED (OrszagId)"); // INDEX
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine(); // Üres sor                                    OrszagId szerint

            // Innen elég jól haladhatunk, megmásolunk egy táblányit és csak módosítunk

            //Helyszín
            stringBuilder.AppendLine("CREATE TABLE Helyszin (");
            stringBuilder.AppendLine("HelyszinId INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("HelyszinNev NVARCHAR(15) NOT NULL,");
            stringBuilder.AppendLine("CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (HelyszinId)");
            stringBuilder.AppendLine(")"); // Üres sor   
            stringBuilder.AppendLine(); // Üres sor

            //Sportolo
            // Tök egyszerűen létrehozható a Helyin táblábó. Kicseréled a Helyszint Sportolora (Kivéve OrszagKod :) )
            stringBuilder.AppendLine("CREATE TABLE Sportolo (");
            stringBuilder.AppendLine("SportoloId INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("SportoloNev NVARCHAR(10) NOT NULL,");
            stringBuilder.AppendLine("OrszagId INT NOT NULL,");
            stringBuilder.AppendLine("CONSTRAINT PK_Sportolo PRIMARY KEY CLUSTERED (SportoloId),");
            // non-clustered index és idegen kulcs                                     helyi mező         kapcsolt tábla és mező
            stringBuilder.AppendLine("CONSTRAINT FK_Sportolo_Orszag FOREIGN KEY (OrszagId) REFERENCES Orszag(OrszagId)");
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine(); // Üres sor

            // Verseny
            stringBuilder.AppendLine("CREATE TABLE Verseny (");
            stringBuilder.AppendLine("VersenyId INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("Datum DATE NOT NULL,");
            stringBuilder.AppendLine("HelyszinId INT NOT NULL,");
            stringBuilder.AppendLine("SportoloId INT NOT NULL,");
            stringBuilder.AppendLine("Eredmeny DECIMAL(6,2) NOT NULL,");
            stringBuilder.AppendLine("Helyezes INT NOT NULL");
            stringBuilder.AppendLine("CONSTRAINT PK_Verseny PRIMARY KEY CLUSTERED (VersenyId), ");
            stringBuilder.AppendLine("CONSTRAINT FK_Verseny_Helyszin FOREIGN KEY (HelyszinId) REFERENCES Helyszin(HelyszinId), ");
            stringBuilder.AppendLine("CONSTRAINT FK_Verseny_Sportolo FOREIGN KEY (SportoloId) REFERENCES Sportolo(SportoloId)");
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine(); // Üres sor


            /*** Rekordok betöltése ***/
            // Betöltés előtt létrehozunk olyan változókat,amelyekben biztosított, hogy egy adat csak egyszer szereljen (Distinct).
            var orszagok = sportolok.Select(s => s.OrszagKod).Distinct();
            var helyszinek = sportolok.Select(s => s.Helyszin).Distinct();
            var sportolokEgyediek = sportolok
                .GroupBy(s => new { s.SportoloNev, s.OrszagKod })
                .Select(g => g.First());

            // Országok
            foreach (var kod in orszagok)
            {                                                          // Az 'N' az SQl szervernek szükséges, hogy a kapott változót
                                                                       // Unicode típusnak kezelje   
                stringBuilder.AppendLine($"INSERT INTO Orszag (OrszagKod) VALUES (N'{kod}');");
            }
            stringBuilder.AppendLine(); // Üres sor

            // Helyszínek
            foreach (var h in helyszinek)
            {
                stringBuilder.AppendLine($"INSERT INTO Helyszin (HelyszinNev) VALUES (N'{h}');");
            }
            stringBuilder.AppendLine(); // Üres sor

            // Sportolók
            foreach (var s in sportolokEgyediek)
            {
                stringBuilder.AppendLine($@"
                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'{s.SportoloNev}', OrszagId FROM Orszag WHERE OrszagKod = N'{s.OrszagKod}';"); //Kikeressük az ID-t
            }
            stringBuilder.AppendLine(); // Üres sor

            // Versenyek
            foreach (var s in sportolok)
            {                                    // Hasonló mint az en-US, csak stabilabb
                var datumStr = s.Datum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var eredmenyStr = s.Eredmeny.ToString(CultureInfo.InvariantCulture);

                stringBuilder.AppendLine($@"
                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '{datumStr}', h.HelyszinId, sp.SportoloId, {eredmenyStr}, {s.Helyezes}
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'{s.Helyszin}' AND sp.SportoloNev = N'{s.SportoloNev}';");
            }

            // Fájl mentése
            File.WriteAllText(@"C:\TEMP\script.sql", stringBuilder.ToString());
            Console.WriteLine(" script.sql sikeresen létrehozva.");
        }

        public static void SqlScriptGeneratorTobbForras(List<string[]> helyszinAdatok, List<string[]> kapcsolatAdatok, List<string[]> rendezvenyAdatok, List<string[]> tipusAdatok)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("-- SQL Script c# alkalmazásból --");

            /*** Adatbázis létrehozása ***/
            stringBuilder.AppendLine("USE master"); // Használatba vesszük az adatbázist
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine("CREATE DATABASE Vizsgazo4");
            stringBuilder.AppendLine("GO"); // Ha nincs go, nem jön létre az adatbázis
            stringBuilder.AppendLine("USE Vizsgazo4"); // Használatba vesszük az adatbázist
            stringBuilder.AppendLine("GO");

            stringBuilder.AppendLine(); // Üres sor

            /*** táblák létrehozása ***/
            //Helyszin
            stringBuilder.AppendLine("CREATE TABLE Helyszin (");
            stringBuilder.AppendLine("Id INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("Nev NVARCHAR(50) NOT NULL");


            /*
             * Indexet hozunk létre a táblának. Index olyan állatfajta, amely megjegyzi a rekordok helyét. (Keresés gyorsítása)
             * A clustered egyedi, csak egy lehet egy táblán, ami az adott mező alapján fizikailag sorbarendezve 
             * tárolja az adatokat
             */            // megszorítás - kötöttség index név  elsődleges e- szerint rendezett              
            stringBuilder.AppendLine("CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (Id)"); // INDEX
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine(); // Üres sor                                    OrszagId szerint

            // Innen elég jól haladhatunk, megmásolunk egy táblányit és csak módosítunk

            //Kapcsolat
            stringBuilder.AppendLine("CREATE TABLE Kapcsolat (");
            stringBuilder.AppendLine("Id INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("Nev NVARCHAR(50) NOT NULL,");
            stringBuilder.AppendLine("Telefon NVARCHAR(15) NOT NULL,");
            stringBuilder.AppendLine("Email NVARCHAR(50) NOT NULL,");
            stringBuilder.AppendLine("Cegnev NVARCHAR(50) NOT NULL,");
            stringBuilder.AppendLine("CONSTRAINT PK_Kapcsolat PRIMARY KEY CLUSTERED (Id)");
            stringBuilder.AppendLine(")"); 
            stringBuilder.AppendLine(); // Üres sor

            //Típus
            stringBuilder.AppendLine("CREATE TABLE Tipus (");
            stringBuilder.AppendLine("Id INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("Nev NVARCHAR(50) NOT NULL,");
            stringBuilder.AppendLine("CONSTRAINT PK_Tipus PRIMARY KEY CLUSTERED (Id),");
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine(); // Üres sor

            // Rendezveny
            stringBuilder.AppendLine("CREATE TABLE Rendezveny (");
            stringBuilder.AppendLine("Id INT IDENTITY(1,1) NOT NULL,"); // Egyedi azonosító, 1-től kezdődően 1-el növekszik
            stringBuilder.AppendLine("KapcsolatId INT NOT NULL,");
            stringBuilder.AppendLine("Idopont DATE NOT NULL,");
            stringBuilder.AppendLine("NapokSzama INT NOT NULL,");
            stringBuilder.AppendLine("HelyszinId INT NOT NULL,");
            stringBuilder.AppendLine("Letszam INT NOT NULL,");
            stringBuilder.AppendLine("TipusId INT NOT NULL");
            stringBuilder.AppendLine("CONSTRAINT PK_Rendezveny PRIMARY KEY CLUSTERED (Id), ");
            stringBuilder.AppendLine("CONSTRAINT FK_Rendezveny_Kapcsolat FOREIGN KEY (KapcsolatId) REFERENCES Kapcsolat(Id), "); 
            stringBuilder.AppendLine("CONSTRAINT FK_Rendezveny_Helyszin  FOREIGN KEY (HelyszinId)  REFERENCES Helyszin(Id),");
            stringBuilder.AppendLine("CONSTRAINT FK_Rendezveny_Tipus     FOREIGN KEY (TipusId)     REFERENCES Tipus(Id)");
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine("GO");
            stringBuilder.AppendLine(); // Üres sor


            /*** Rekordok betöltése ***/
            /* Erre itt nincs szükség, hiszen konkrét tábla adatokkal dolgozunk
            // Betöltés előtt létrehozunk olyan változókat,amelyekben biztosított, hogy egy adat csak egyszer szereljen (Distinct).
            var orszagok = sportolok.Select(s => s.OrszagKod).Distinct();
            var helyszinek = sportolok.Select(s => s.Helyszin).Distinct();
            var sportolokEgyediek = sportolok
                .GroupBy(s => new { s.SportoloNev, s.OrszagKod })
                .Select(g => g.First());

            */

            // Helyszin
            stringBuilder.AppendLine("SET IDENTITY_INSERT Helyszin ON"); // Tiltjuk az Id automatikus beszúrását (ON - engedélyezzük magunknak)
            foreach (var item in helyszinAdatok)
            {
                stringBuilder.AppendLine($"INSERT INTO Helyszin (Id, Nev) VALUES({item[0]}, N'{item[1]}')");
            }
            stringBuilder.AppendLine("SET IDENTITY_INSERT Helyszin OFF");
            stringBuilder.AppendLine(); // Üres sor

            // Kapcsolat
            stringBuilder.AppendLine("SET IDENTITY_INSERT Kapcsolat ON");
            foreach (var item in kapcsolatAdatok)
            {
                stringBuilder.AppendLine($"INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES({item[0]}, N'{item[1]}', N'{item[2]}', N'{item[3]}', N'{item[4]}')");
            }
            stringBuilder.AppendLine("SET IDENTITY_INSERT Kapcsolat OFF");
            stringBuilder.AppendLine(); // Üres sor

            // Tipus
            stringBuilder.AppendLine("SET IDENTITY_INSERT Tipus ON");
            foreach (var item in tipusAdatok)
            {
                stringBuilder.AppendLine($"INSERT INTO Tipus (Id, Nev) VALUES({item[0]}, N'{item[1]}')");
            }
            stringBuilder.AppendLine("SET IDENTITY_INSERT Tipus OFF");
            stringBuilder.AppendLine(); // Üres sor

            // Rendezvény
            stringBuilder.AppendLine("SET IDENTITY_INSERT Rendezveny ON");
            foreach (var item in rendezvenyAdatok)
            {
                string datumStr = item[2].Replace('.', '-'); // SQL-nek megfelelő dátumformátum
                stringBuilder.AppendLine($"INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES({item[0]}, {item[1]}, '{datumStr}', {item[3]}, {item[4]}, {item[5]}, {item[6]})");
            }
            stringBuilder.AppendLine("SET IDENTITY_INSERT Rendezveny OFF");

            // Fájl mentése
            File.WriteAllText(@"C:\TEMP\script.sql", stringBuilder.ToString());
            Console.WriteLine(" script.sql sikeresen létrehozva.");
        }
    }
}
