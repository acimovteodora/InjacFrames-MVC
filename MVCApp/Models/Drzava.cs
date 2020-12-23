using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Drzava : IDomainObject
    {
        public long Id { get; set; }
        [DisplayName("Naziv države")]
        public string NazivDrzave { get; set; }

        public string TabelName => "Drzava";

        public string InsertColumns => throw new NotImplementedException();

        public string InsertValue => throw new NotImplementedException();

        public string ColumnNames => "SifraDrzave,NazivDrzave";

        public string SetColumnValues => throw new NotImplementedException();

        public string SearchCondition => $" WHERE SifraDrzave={Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "OUTPUT inserted.SifraDrzave";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> countries = new List<IDomainObject>();
                while (reader.Read())
                {
                    Drzava country = new Drzava
                    {
                        Id = (long)reader[0],
                        NazivDrzave = reader[1].ToString()
                    };
                    countries.Add(country);
                }
                return countries;
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
                Drzava country = new Drzava();
                while (reader.Read())
                {
                    country.Id = (long)reader[0];
                    country.NazivDrzave = reader[1].ToString();
                    break;
                }
                return country;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}