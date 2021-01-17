using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface IAdresaLogic
    {
        List<Adresa> SelectAll(Adresa objekat);
        Adresa SelectObject(Adresa objekat);
        bool CreateObject(Adresa objekat);
        bool UpdateObject(Adresa objekat);
        List<Grad> GetCities();
        List<Kompanija> GetCompanies();
    }
}
