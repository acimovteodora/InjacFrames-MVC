using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class StavkaPorudzbine
    {
        public long RedniBroj { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public int Kolicina { get; set; }
        public Lajsna Lajsna { get; set; }
    }
}