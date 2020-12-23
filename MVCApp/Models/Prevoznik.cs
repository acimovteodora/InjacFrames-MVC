using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Prevoznik : Kompanija, IDomainObject
    {
        public int BrojPrevoza { get; set; }
        public new string TabelName => "Prevoznik";

        public new string InsertColumns => "BrojPrevoza";

        public new string InsertValue => $"VALUES ({BrojPrevoza})";

        public new string ColumnNames => "SifraKompanije, BrojPrevoza";

        public new string SetColumnValues => $"BrojPrevoza={BrojPrevoza}";

        public new string JoinSelect => "p.SifraKompanije, k.NazivKompanije";

        public new string JoinTables => "FROM Prevoznik p JOIN Kompanija k ON p.SifraKompanije=k.SifraKompanije";

        public new List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> companies = new List<IDomainObject>();
                while (reader.Read())
                {
                    Prevoznik company = new Prevoznik
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
                Prevoznik company = new Prevoznik();
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