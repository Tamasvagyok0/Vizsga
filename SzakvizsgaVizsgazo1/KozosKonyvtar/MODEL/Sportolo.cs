using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class Sportolo
    {
        [Key]
        public int SportoloId { get; set; }
        public string SportoloNev { get; set; } = string.Empty;
        public int OrszagId { get; set; }
    }
}
