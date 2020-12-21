using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Faktura
    {
        public long Id { get; set; }
        public DateTime DatumSastavljanja { get; set; }
        public double NetoTezina { get; set; }
        public double BrutoTezina { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Proforma Proforma { get; set; }
    }
}