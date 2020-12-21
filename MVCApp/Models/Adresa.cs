using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Adresa : IDomainObject
    {
        public Kompanija Kompanija { get; set; }
        public Grad Grad { get; set; }
        public long SifraAdrese { get; set; }
        public string Naziv { get; set; }
        public string Broj { get; set; }
        public string NazivGrada { get; set; }

        public string TabelName => "Adresa";

        public string InsertValue => $" '{Kompanija.Id}','{Grad.Id}','{Naziv}','{Broj}','{NazivGrada}'";

        public string SearchCondition => $" where SifraAdrese = {SifraAdrese}";

        public string ColumnNames => "SifraKompanije, SifraGrada, Naziv, Broj, NazivGrada";

        public string SetColumnValues => $" SifraKompanije={Kompanija.Id},SifraGrada={Grad.Id},Naziv='{Naziv}',Broj='{Broj}',NazivGrada='{NazivGrada}'";

        public string IdColumn => "SifraAdrese";

        public int Id => 0;

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> adresses = new List<IDomainObject>();
                while (reader.Read())
                {
                    Adresa adress = new Adresa();
                    adress.SifraAdrese = (long)reader[0];
                    adress.Kompanija = new Kompanija() { Id = (long)reader[1] }; 
                    adress.Grad = new Grad() { Id = (long)reader[2] };
                    adress.Naziv = reader[3].ToString();
                    adress.Broj = reader[4].ToString();
                    adress.NazivGrada = reader[5].ToString();
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
                    adress.Kompanija = new Kompanija() { Id = (long)reader[1] };
                    adress.Grad = new Grad() { Id = (long)reader[2] };
                    adress.Naziv = reader[3].ToString();
                    adress.Broj = reader[4].ToString();
                    adress.NazivGrada = reader[5].ToString();
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