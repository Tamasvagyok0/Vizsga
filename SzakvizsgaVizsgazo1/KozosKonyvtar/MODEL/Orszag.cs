using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class Orszag
    {
        [Key]
        public int OrszagId { get; set; }
        public String OrszagKod { get; set; } = String.Empty;

    }
}
