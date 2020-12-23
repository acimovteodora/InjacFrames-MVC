using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class StavkaProforme : IDomainObject
    {
        public long RedniBroj { get; set; }
        public Proforma Proforma { get; set; }
        public int Kolicina { get; set; }
        public Lajsna Lajsna { get; set; }

        public string Identifikator => "";

        public string TabelName => "StavkaProforme";

        public string InsertColumns => "SifraProforme,Kolicina,SifraLajsne";

        public string InsertValue => $"VALUES ({Proforma.Id},{Kolicina},{Lajsna.Id})";

        public string ColumnNames => "RedniBroj,SifraProforme,Kolicina,SifraLajsne";

        public string SetColumnValues => $"Kolicina={Kolicina},SifraLajsne={Lajsna.Id}";

        public string SearchCondition => $"WHERE RedniBroj={RedniBroj}";

        public string JoinSelect => "sp.RedniBroj, sp.SifraProforme, sp.Kolicina, l.SifraLajsne, l.NazivLajsne";

        public string JoinTables => "FROM StavkaProforme sp JOIN Lajsna l ON sp.SifraLajsne=l.SifraLajsne";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> stavke = new List<IDomainObject>();
                while (reader.Read())
                {
                    StavkaProforme stavka = new StavkaProforme
                    {
                        RedniBroj = (long)reader[0],
                        Proforma = new Proforma() { Id = (long)reader[1] },
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
                StavkaProforme stavka = new StavkaProforme();
                while (reader.Read())
                {
                    stavka.RedniBroj = (long)reader[0];
                    stavka.Proforma = new Proforma() { Id = (long)reader[1] };
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