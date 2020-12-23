using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Grad : IDomainObject
    {
        [DisplayName("Šifra grada")]
        public long Id { get; set; }
        [DisplayName("Naziv grada")]
        public string NazivGrada { get; set; }
        [DisplayName("Poštanski broj")]
        public string PostanskiBroj { get; set; }
        public Drzava Drzava { get; set; }

        public string TabelName => "Grad";

        public string InsertColumns => "NazivGrada,SifraDrzave,PostanskiBroj";

        public string InsertValue => $" VALUES('{NazivGrada}', {Drzava.Id}, '{PostanskiBroj}')";

        public string ColumnNames => "SifraGrada,NazivGrada,SifraDrzave,PostanskiBroj";

        public string SetColumnValues => $"NazivGrada='{NazivGrada}', SifraDrzave={Drzava.Id}, PostanskiBroj='{PostanskiBroj}'";

        public string SearchCondition => $"WHERE SifraGrada={Id}";

        public string JoinSelect => "g.SifraGrada, g.NazivGrada, g.PostanskiBroj, g.SifraDrzave, d.NazivDrzave";

        public string JoinTables => "FROM Grad g JOIN Drzava d ON g.SifraDrzave=d.SifraDrzave";

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> cities = new List<IDomainObject>();
                while (reader.Read())
                {
                    Grad city = new Grad
                    {
                        Id = (long)reader[0],
                        NazivGrada = reader[1].ToString(),
                        Drzava = new Drzava() { Id = (long)reader[3], NazivDrzave = reader[4].ToString() },
                        PostanskiBroj = reader[2].ToString()
                    };
                    cities.Add(city);
                }
                return cities;
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
                Grad city = new Grad();
                while (reader.Read())
                {
                    city.Id = (long)reader[0];
                    city.NazivGrada = reader[1].ToString();
                    city.Drzava = new Drzava() { Id = (long)reader[2] };
                    city.PostanskiBroj = reader[3].ToString();
                    break;
                }
                return city;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}