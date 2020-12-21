using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Kontakt
    {
        public long Id { get; set; }
        public Kompanija Kompanija{ get; set; }
        public string Vrednost { get; set; }
        public TipKontakta TipKontakta { get; set; }
    }
}