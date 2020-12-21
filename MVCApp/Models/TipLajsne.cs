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
        [DisplayName("Sifra tipa lajsne")]
        public long Id { get; set; }
        [DisplayName("Naziv tipa lajsne")]
        public string NazivTipa { get; set; }
        public Dimenzije Dimenzije { get; set; }

        public string TabelName => "TipLajsne";

        public string InsertValue => "";

        public string SearchCondition => $" WHERE SifraTipa = {Id}";

        public string ColumnNames => "SifraTipa,NazivTipa,Dimenzije";

        public string SetColumnValues => $"NazivTipa = {NazivTipa}, Dimenzije='{Dimenzije.Duzina};{Dimenzije.Visina}'";

        public string IdColumn => "SifraTipa";

        int IDomainObject.Id => 0;

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> types = new List<IDomainObject>();
                while (reader.Read())
                {
                    TipLajsne type = new TipLajsne();
                    type.Id = (long)reader[0];
                    type.NazivTipa = reader[1].ToString();
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
            throw new NotImplementedException();
        }
    }
}