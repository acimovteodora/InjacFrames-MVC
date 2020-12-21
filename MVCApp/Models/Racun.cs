using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Racun
    {
        public long Id { get; set; }
        public Banka Banka { get; set; }
        public string Vrednost { get; set; }
    }
}