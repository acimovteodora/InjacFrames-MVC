using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Klijent : Kompanija, IDomainObject
    {
        public int BrojPorudzbina { get; set; }
        public new string TabelName => "Klijent";

        public new string InsertColumns => "BrojPorudzbina";

        public new string InsertValue => $" VALUES({BrojPorudzbina})";

        public new string ColumnNames => "SifraKompanije, BrojPorudzbina";

        public new string SetColumnValues => $"BrojPorudzbina={BrojPorudzbina}";

        public new string JoinSelect => "c.SifraKompanije,k.NazivKompanije";

        public new string JoinTables => "FROM Klijent c JOIN Kompanija k ON c.SifraKompanije=k.SifraKompanije";

        public new List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> companies = new List<IDomainObject>();
                while (reader.Read())
                {
                    Klijent company = new Klijent
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
                Klijent company = new Klijent();
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