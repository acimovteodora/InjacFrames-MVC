using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DataAccessLayer.Interfaces
{
    public interface IProformaLogic
    {
        List<Proforma> SelectAll(Proforma objekat);
        Proforma SelectObject(Proforma objekat);
        bool UpdateObject(Proforma objekat);
        bool CreateObject(Racun objekat);
        List<Racun> GetRacuni();
        List<NacinIsporuke> GetNaciniIsporuke();
        List<Lajsna> GetLajsne();
        List<Banka> GetBanke();
        List<Lajsna> GetLajsne(List<Lajsna> lajsne);
        Lajsna GetLajsna(string name);
    }
}
