using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface IStavkaProformeLogic
    {
        List<Lajsna> GetLajsne();
        bool UpdateObject(StavkaProforme objekat);
        bool DeleteObject(StavkaProforme objekat);
        bool CreateObject(StavkaProforme objekat);
        StavkaProforme SelectObject(StavkaProforme objekat);
    }
}
