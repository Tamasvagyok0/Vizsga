using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.VIEWMODEL
{
    public class SportoloViewModel
    {
        public int Id { get; set; }
        public string SportoloNev { get; set; } = string.Empty;
        public string OrszagKod { get; set; } = string.Empty;
        public string Helyszin { get; set; } = string.Empty;
        public DateOnly Datum { get; set; }
        public decimal Eredmeny { get; set; }
        public int Helyezes { get; set; }
    }
}
