using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class Verseny
    {
        [Key]
        public int VersenyId { get; set; }
        public DateOnly Datum { get; set; }
        public int SportoloId { get; set; }
        public int HelyszinId { get; set; }
        public int Helyezes { get;set; }
        public Decimal Eredmeny { get; set; }
    }
}
