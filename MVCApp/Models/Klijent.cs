using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Klijent : Kompanija
    {
        public int BrojPorudzbina { get; set; }
    }
}