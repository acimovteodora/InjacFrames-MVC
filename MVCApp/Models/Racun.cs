using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Racun : IDomainObject
    {
        public long Id { get; set; }
        public Banka Banka { get; set; }
        public string Vrednost { get; set; }

        public string Identifikator => "OUTPUT inserted.SifraRacuna";

        public string TabelName => "Racun";

        public string InsertColumns => "SifraBanke,BrojRacuna";

        public string InsertValue => $"VALUES ({Banka.Id},'{Vrednost}')";

        public string ColumnNames => "SifraRacuna,SifraBanke,BrojRacuna";

        public string SetColumnValues => $"SifraBanke={Banka.Id},BrojRacuna='{Vrednost}'";

        public string SearchCondition => $"WHERE SifraRacuna = {Id}";

        public string JoinSelect => "r.SifraRacuna,r.BrojRacuna,r.SifraBanke,b.Naziv,b.Swift";

        public string JoinTables => "from Racun r join Banka b on r.SifraBanke=b.SifraBanke";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> racuni = new List<IDomainObject>();
                while (reader.Read())
                {
                    Racun racun = new Racun
                    {
                        Id = (long)reader[0],
                        Vrednost = reader[1].ToString(),
                        Banka = new Banka() { Id = (long)reader[2], Naziv = reader[3].ToString(), Swift = reader[4].ToString() }
                    };
                    racuni.Add(racun);
                }
                return racuni;
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
                Racun racun = new Racun();
                while (reader.Read())
                {
                    racun.Id = (long)reader[0];
                    racun.Vrednost = reader[1].ToString();
                    racun.Banka = new Banka() { Id = (long)reader[2], Naziv = reader[3].ToString(), Swift = reader[4].ToString() };
                    break;
                }
                return racun;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}