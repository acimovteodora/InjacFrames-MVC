using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface IPorudzbinaLogic
    {
        List<Porudzbina> SelectAll(Porudzbina objekat);
        Porudzbina SelectObject(Porudzbina objekat);
        List<Porudzbina> SelectByCriteria(string criteria, Porudzbina objekat);
        bool CreateObject(Porudzbina objekat);
        //bool UpdateObject(Porudzbina objekat);
        //bool DeleteObject(Porudzbina objekat);
        List<Klijent> GetKlijenti();
        List<NacinIsporuke> GetNaciniIsporuke();
        List<NacinPlacanja> GetNaciniPlacanja();
        List<Lajsna> GetLajsne();
        Lajsna GetLajsna(string name);
        Katalog GetKatalog(string criteria);
    }
}
