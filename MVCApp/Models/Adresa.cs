using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Adresa : IDomainObject
    {
        public Kompanija Kompanija { get; set; }
        public Grad Grad { get; set; }
        [DisplayName("Šifra adrese")]
        public long SifraAdrese { get; set; }
        public string Naziv { get; set; }
        public string Broj { get; set; }
        [DisplayName("Grad")]
        public string NazivGrada { get; set; }

        public string TabelName => "Adresa";
        public string InsertColumns => "SifraKompanije,SifraGrada,Naziv,Broj,NazivGrada";

        public string InsertValue => $" VALUES({Kompanija.Id},{Grad.Id},'{Naziv}','{Broj}','{NazivGrada}')";

        public string SearchCondition => $" WHERE SifraAdrese = {SifraAdrese}";

        public string ColumnNames => "SifraAdrese,SifraKompanije, SifraGrada, Naziv, Broj";

        public string SetColumnValues => $" SifraKompanije={Kompanija.Id},SifraGrada={Grad.Id},Naziv='{Naziv}',Broj='{Broj}'";

        public string JoinSelect =>"a.SifraAdrese, k.SifraKompanije, k.NazivKompanije, a.SifraGrada, a.NazivGrada, a.Naziv, a.Broj";

        public string JoinTables =>  "FROM Adresa a JOIN Kompanija k ON a.SifraKompanije=k.SifraKompanije";

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> adresses = new List<IDomainObject>();
                while (reader.Read())
                {
                    Adresa adress = new Adresa
                    {
                        SifraAdrese = (long)reader[0],
                        Kompanija = new Kompanija() { Id = (long)reader[1], NazivKompanije = reader[2].ToString() },
                        Grad = new Grad() { Id = (long)reader[3] },
                        NazivGrada = reader[4].ToString(),
                        Naziv = reader[5].ToString(),
                        Broj = reader[6].ToString()
                    };
                    adresses.Add(adress);
                }
                return adresses;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IDomainObject VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Adresa adress = new Adresa();
                while (reader.Read())
                {
                    adress.SifraAdrese = (long)reader[0];
                    adress.Kompanija = new Kompanija() { Id = (long)reader[1]};
                    adress.Grad = new Grad() { Id = (long)reader[2] };
                    adress.NazivGrada = reader[5].ToString();
                    adress.Naziv = reader[3].ToString();
                    adress.Broj = reader[4].ToString();
                    break;
                }
                return adress;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}