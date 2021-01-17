using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface ITipLajsneLogic
    {
        List<TipLajsne> SelectAll(TipLajsne objekat);
        TipLajsne SelectObject(TipLajsne objekat);
        bool CreateObject(TipLajsne objekat);
        bool UpdateObject(TipLajsne objekat);
        //int DeleteObject(TipLajsne objekat);
        //List<TipLajsne> ReturnByCriteria(string criteria, TipLajsne objekat);
    }
}
