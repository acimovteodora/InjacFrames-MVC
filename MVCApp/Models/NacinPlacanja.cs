using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class NacinPlacanja : IDomainObject
    {
        public long Id { get; set; }
        public string NazivPlacanja { get; set; }

        public string TabelName => "NacinPlacanja";

        public string InsertColumns => "NazivPlacanja";

        public string InsertValue => $" VALUES('{NazivPlacanja}')";

        public string ColumnNames => "SifraPlacanja,NazivPlacanja";

        public string SetColumnValues => $"NazivPlacanja='{NazivPlacanja}'";

        public string SearchCondition => $"WHERE SifraPlacanja={Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> placanja = new List<IDomainObject>();
                while (reader.Read())
                {
                    NacinPlacanja placanje = new NacinPlacanja
                    {
                        Id = (long)reader[0],
                        NazivPlacanja = reader[1].ToString()
                    };
                    placanja.Add(placanje);
                }
                return placanja;
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
                NacinPlacanja placanje = new NacinPlacanja();
                while (reader.Read())
                {
                    placanje.Id = (long)reader[0];
                    placanje.NazivPlacanja = reader[1].ToString();
                    break;
                }
                return placanje;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}