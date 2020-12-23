using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Banka : IDomainObject
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Swift { get; set; }

        public string Identifikator => "";

        public string TabelName => "Banka";

        public string InsertColumns => throw new NotImplementedException();

        public string InsertValue => throw new NotImplementedException();

        public string ColumnNames => "SifraBanke,Naziv";

        public string SetColumnValues => throw new NotImplementedException();

        public string SearchCondition => $" WHERE SifraBanke={Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> banks = new List<IDomainObject>();
                while (reader.Read())
                {
                    Banka bank = new Banka
                    {
                        Id = (long)reader[0],
                        Naziv = reader[1].ToString()
                    };
                    banks.Add(bank);
                }
                return banks;
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
                Banka bank = new Banka();
                while (reader.Read())
                {
                    bank.Id = (long)reader[0];
                    bank.Naziv = reader[1].ToString();
                    break;
                }
                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}