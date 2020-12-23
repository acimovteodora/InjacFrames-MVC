using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class StavkaPorudzbine : IDomainObject
    {
        public long RedniBroj { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public int Kolicina { get; set; }
        public Lajsna Lajsna { get; set; }

        public string TabelName => "StavkaPorudzbine";

        public string InsertColumns => "SifraPorudzbine,Kolicina,SifraLajsne";

        public string InsertValue => $" VALUES ({Porudzbina.Id},{Kolicina},{Lajsna.Id})";

        public string ColumnNames => "RedniBroj,SifraPorudzbine,Kolicina,SifraLajsne";

        public string SetColumnValues => $"Kolicina={Kolicina},SifraLajse={Lajsna.Id}";

        public string SearchCondition => $"WHERE RedniBroj={RedniBroj}";

        public string JoinSelect => "sp.RedniBroj, sp.SifraPorudzbine, sp.Kolicina, l.SifraLajsne, l.NazivLajsne";

        public string JoinTables => "FROM StavkaPorudzbine sp JOIN Lajsna l ON sp.SifraLajsne=l.SifraLajsne";

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> stavke = new List<IDomainObject>();
                while (reader.Read())
                {
                    StavkaPorudzbine stavka = new StavkaPorudzbine
                    {
                        RedniBroj = (long)reader[0],
                        Porudzbina = new Porudzbina() { Id = (long)reader[1]},
                        Kolicina = (int)reader[2],
                        Lajsna = new Lajsna() { Id = (long)reader[3], NazivLajsne = reader[4].ToString() }
                    };
                    stavke.Add(stavka);
                }
                return stavke;
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
                StavkaPorudzbine stavka = new StavkaPorudzbine();
                while (reader.Read())
                {
                    stavka.RedniBroj = (long)reader[0];
                    stavka.Porudzbina = new Porudzbina() { Id = (long)reader[1] };
                    stavka.Kolicina = (int)reader[2];
                    stavka.Lajsna = new Lajsna() { Id = (long)reader[3], NazivLajsne = reader[4].ToString() };
                    break;
                }
                return stavka;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}