using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public interface INalogLogic
    {
        List<NalogZaUtovar> SelectAll(NalogZaUtovar objekat);
        NalogZaUtovar SelectObject(NalogZaUtovar objekat);
        bool CreateObject(NalogZaUtovar objekat);
        bool UpdateObject(NalogZaUtovar objekat);
        bool DeleteObject(NalogZaUtovar objekat);
        List<Prevoznik> GetPrevoznici();
        List<Carinik> GetCarinici();
        List<Porudzbina> GetPorudzbineUpdate();
        List<Porudzbina> GetPorudzbineInsert();
    }
}
