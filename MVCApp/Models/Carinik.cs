using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Carinik : Kompanija,IDomainObject
    {
        public int BrojCarinjenja { get; set; }
        public new string TabelName => "Carinik";

        public new string InsertColumns => "BrojCarinjenja";

        public new string InsertValue => $" VALUES({BrojCarinjenja})";

        public new string ColumnNames => "SifraKompanije, BrojCarinjenja";

        public new string SetColumnValues => $"BrojCarinjenja={BrojCarinjenja}";

        public new string JoinSelect => "c.SifraKompanije,k.NazivKompanije";

        public new string JoinTables => "FROM Carinik c JOIN Kompanija k ON c.SifraKompanije=k.SifraKompanije";

        public new List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> companies = new List<IDomainObject>();
                while (reader.Read())
                {
                    Carinik company = new Carinik
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

        public new IDomainObject VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Carinik company = new Carinik();
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