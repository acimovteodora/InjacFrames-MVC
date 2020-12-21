using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class StavkaKataloga
    {
        public Katalog Katalog { get; set; }
        public long RedniBroj { get; set; }
        public Lajsna Lajsna { get; set; }

    }
}