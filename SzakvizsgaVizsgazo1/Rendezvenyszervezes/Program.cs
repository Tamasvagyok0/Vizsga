using KozosKonyvtar;
using KozosKonyvtar.MODEL;

namespace Rendezvenyszervezes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Változók meghatározása
            char elvalaszto = ';';
            bool   vanFejlec = true;

            List<string[]> helyszinAdatok = KozosKonyvtar.FajlKezeles.OlvasFajlbol("helyszin.csv", elvalaszto, vanFejlec); // A soron 3 * Ctrl + D
            List<string[]> kapcsolatAdatok = KozosKonyvtar.FajlKezeles.OlvasFajlbol("kapcsolat.csv", elvalaszto, vanFejlec);
            List<string[]> rendezvenyAdatok = KozosKonyvtar.FajlKezeles.OlvasFajlbol("rendezveny.csv", elvalaszto, vanFejlec);
            List<string[]> tipusAdatok = KozosKonyvtar.FajlKezeles.OlvasFajlbol("tipus.csv", elvalaszto, vanFejlec);

            Console.WriteLine("SQL script készítés");
            // Figyelj!!! Az adatlistákat olyan sorremdben add meg, mint ahogy a metódusban felsorolod!!!
            // (Vagy készíthetsz minden táblára egy külön metódust :) )
            FajlKezeles.SqlScriptGeneratorTobbForras(helyszinAdatok, kapcsolatAdatok, rendezvenyAdatok, tipusAdatok);

        }
    }
}
