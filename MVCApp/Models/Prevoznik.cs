using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Prevoznik : Kompanija
    {
        public int BrojPrevoza { get; set; }
    }
}