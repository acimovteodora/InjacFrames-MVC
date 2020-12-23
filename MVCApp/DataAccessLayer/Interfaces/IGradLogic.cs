using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DataAccessLayer.Interfaces
{
    public interface IGradLogic
    {
        List<Grad> SelectAll(Grad objekat);
        Grad SelectObject(Grad objekat);
        bool UpdateObject(Grad objekat);
        List<Drzava> GetCountries();
    }
}
