using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class StavkaProforme
    {
        public long RedniBroj { get; set; }
        public Proforma Proforma { get; set; }
        public int Kolicina { get; set; }
        public Lajsna Lajsna { get; set; }
    }
}