using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface ICenaLogic
    {
        //List<Cena> SelectAll(Cena objekat);
        List<Cena> SelectAll(Cena objekat);
        Cena SelectObject(Cena objekat);
        bool CreateObject(Cena objekat);
        bool UpdateObject(Cena objekat);
        bool DeleteObject(Cena objekat);
        List<Lajsna> GetLajsne();
    }
}
