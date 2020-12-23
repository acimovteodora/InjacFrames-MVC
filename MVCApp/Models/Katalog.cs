using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Katalog : IDomainObject
    {
        public long Id { get; set; }
        public int Godina { get; set; }
        public List<StavkaKataloga> Stavke { get; set; }

        public string TabelName => "Katalog";

        public string InsertColumns => "Godina";

        public string InsertValue => $" VALUES({Godina})";

        public string ColumnNames => "SifraKataloga,Godina";

        public string SetColumnValues => $"Godina={Godina}";

        public string SearchCondition => $"WHERE SifraKataloga={Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> katalozi = new List<IDomainObject>();
                while (reader.Read())
                {
                    Katalog katalog = new Katalog
                    {
                        Id = (long)reader[0],
                        Godina = (int)reader[1]
                    };
                    katalozi.Add(katalog);
                }
                return katalozi;
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
                Katalog katalog = new Katalog();
                while (reader.Read())
                {
                    katalog.Id = (long)reader[0];
                    katalog.Godina = (int)reader[1];
                    break;
                }
                return katalog;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}