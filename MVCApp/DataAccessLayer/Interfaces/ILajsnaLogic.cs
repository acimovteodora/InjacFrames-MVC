using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DataAccessLayer
{
    public interface ILajsnaLogic
    {
        List<Lajsna> SelectAll(Lajsna objekat);
        Lajsna SelectObject(Lajsna objekat);
        bool CreateObject(Lajsna objekat);
        bool UpdateObject(Lajsna objekat);
        int DeleteObject(Lajsna objekat);
        List<Lajsna> SelectByCriteria(string criteria, Lajsna objekat);
        List<TipLajsne> GetTipoviLajsni();
    }
}
