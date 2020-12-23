using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Kompanija : IDomainObject
    {
        [DisplayName("Šifra kompanije")]
        public long Id { get; set; }
        [DisplayName("Kompanija")]
        public string NazivKompanije { get; set; }

        public string TabelName => "Kompanija";

        public string InsertColumns => throw new NotImplementedException();

        public string InsertValue => throw new NotImplementedException();

        public string ColumnNames => "SifraKompanije, NazivKompanije";

        public string SetColumnValues => throw new NotImplementedException();

        public string SearchCondition => throw new NotImplementedException();

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> companies = new List<IDomainObject>();
                while (reader.Read())
                {
                    Kompanija company = new Kompanija
                    {
                        Id = (long)reader[0],
                        NazivKompanije = reader[1].ToString()
                    };
                    companies.Add(company);
                }
                return companies;
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
                Kompanija company = new Kompanija();
                while (reader.Read())
                {
                    company.Id = (long)reader[0];
                    company.NazivKompanije = reader[1].ToString();
                    break;
                }
                return company;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}