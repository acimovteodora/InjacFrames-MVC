using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Grad
    {
        public long Id { get; set; }
        public string NazivGrada { get; set; }
        public string PostanskiBroj { get; set; }
        public Drzava Drzava { get; set; }
    }
}