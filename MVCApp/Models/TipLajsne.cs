using Assembly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class TipLajsne: IDomainObject
    {
        [DisplayName("Šifra tipa lajsne")]
        public long Id { get; set; }
        [DisplayName("Naziv tipa lajsne")]
        public string NazivTipa { get; set; }
        public Dimenzije Dimenzije { get; set; }

        public string TabelName => "TipLajsne";

        public string InsertColumns => "NazivTipa,Dimenzije";

        public string InsertValue => $" VALUES ('{NazivTipa}','{Dimenzije}')";

        public string SearchCondition => $" WHERE SifraTipa = {Id}";

        public string ColumnNames => "SifraTipa,NazivTipa,Dimenzije";

        public string SetColumnValues => $"NazivTipa = '{NazivTipa}', Dimenzije='{Dimenzije}'";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "SifraTipa";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> types = new List<IDomainObject>();
                while (reader.Read())
                {
                    TipLajsne type = new TipLajsne
                    {
                        Id = (long)reader[0],
                        NazivTipa = reader[1].ToString()
                    };
                    string[] dimn = reader[2].ToString().Split(";");
                    type.Dimenzije = new Dimenzije() { Duzina = Convert.ToInt32(dimn[0]), Visina = Convert.ToInt32(dimn[1]) };
                    types.Add(type);
                }
                return types;
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
                TipLajsne type = new TipLajsne();
                while (reader.Read())
                {
                    type.Id = (long)reader[0];
                    type.NazivTipa = reader[1].ToString();
                    string[] dimn = reader[2].ToString().Split(";");
                    type.Dimenzije = new Dimenzije() { Duzina = Convert.ToInt32(dimn[0]), Visina = Convert.ToInt32(dimn[1]) };
                    break;
                }
                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}