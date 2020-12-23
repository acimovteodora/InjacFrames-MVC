using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class NacinIsporuke : IDomainObject
    {
        public long Id { get; set; }
        public string NazivIsporuke { get; set; }
        public string TabelName => "NacinIsporuke";

        public string InsertColumns => "NazivIsporuke";

        public string InsertValue => $" VALUES('{NazivIsporuke}')";

        public string ColumnNames => "SifraIsporuke,NazivIsporuke";

        public string SetColumnValues => $"NazivIsporuke='{NazivIsporuke}'";

        public string SearchCondition => $"WHERE SifraIsporuke={Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> isporuke = new List<IDomainObject>();
                while (reader.Read())
                {
                    NacinIsporuke isporuka = new NacinIsporuke
                    {
                        Id = (long)reader[0],
                        NazivIsporuke = reader[1].ToString()
                    };
                    isporuke.Add(isporuka);
                }
                return isporuke;
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
                NacinIsporuke isporuka = new NacinIsporuke();
                while (reader.Read())
                {
                    isporuka.Id = (long)reader[0];
                    isporuka.NazivIsporuke = reader[1].ToString();
                    break;
                }
                return isporuka;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}