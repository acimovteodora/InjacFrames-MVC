using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DataAccessLayer
{
    public interface ILajsnaLogic
    {
        List<Lajsna> ReturnAll(Lajsna objekat);
        List<TipLajsne> GetTipoviLajsni();
        Lajsna ReturnObject(Lajsna objekat);
        bool UpdateObject(Lajsna objekat);
        int DeleteObject(Lajsna objekat);
        List<Lajsna> ReturnByCriteria(string criteria, Lajsna objekat);
    }
}
