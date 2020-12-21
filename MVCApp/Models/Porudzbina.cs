using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Porudzbina
    {
        public long Id { get; set; }
        public DateTime Datum { get; set; }
        public string UsloviIsporuke { get; set; }
        public Klijent Klijent { get; set; }
        public Katalog Katalog { get; set; }
        public NacinIsporuke NacinIsporuke { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
    }
}