using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.VIEWMODEL
{
    public class VersenyViewModel
    {
        public int VersenyId { get; set; }
        public string SportoloNev { get; set; } = string.Empty;
        public string OrszagKod { get; set; } = string.Empty;    
        public string HelyszinNev { get; set; } = string.Empty;
        public DateOnly Datum { get; set; }
        public decimal Eredmeny { get; set; }
        public int Helyezes { get; set; }
        public string DatumSzoveg => Datum.ToString("yyyy. MMMM dd.", new CultureInfo("hu-HU"));
    }
}
