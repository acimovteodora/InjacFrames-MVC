using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Proforma
    {
        public long Id { get; set; }
        public DateTime Datum { get; set; }
        public int Popust { get; set; }
        public int UkupnaCena { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public NacinIsporuke NacinIsporuke { get; set; }
        public Racun Racun { get; set; }
    }
}