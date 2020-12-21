using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class NalogZaUtovar
    {
        public long Id { get; set; }
        public int BrojPaleta { get; set; }
        public string DimenzijePalete { get; set; }
        public double NetoTezinaPalete { get; set; }
        public DateTime DatumVremeUtovara { get; set; }
        public int CenaTransporta { get; set; }
        public string Napomena { get; set; }
        public Carinik Carinik { get; set; }
        public Prevoznik Prevoznik { get; set; }
        public Porudzbina Porudzbina { get; set; }
    }
}