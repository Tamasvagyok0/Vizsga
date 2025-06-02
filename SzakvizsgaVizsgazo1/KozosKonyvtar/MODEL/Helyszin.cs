using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class Helyszin
    {
        [Key]
        public int HelyszinId { get; set; }
        public String HelyszinNev { get; set; } = String.Empty;
    }
}
