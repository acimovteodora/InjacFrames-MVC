using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Lajsna : IDomainObject
    {
        [DisplayName("Šifra lajsne")]
        public long Id { get; set; }
        [DisplayName("Naziv lajsne")]
        public string NazivLajsne { get; set; }
        [DisplayName("Naziv tipa lajsne")]
        public string NazivTipa { get; set; }
        public string UrlSlike { get; set; }
        [DisplayName("Aktuelna cena(metar)")]
        public int? TrenutnaCena { get; set; }
        [DisplayName("Šifra tipa lajsne")]
        public TipLajsne TipLajsne { get; set; }

        public string TabelName => "Lajsna";
        public string InsertColumns => "NazivLajsne,SifraTipa";

        public string InsertValue => $" VALUES ({NazivLajsne},{TipLajsne.Id})";

        public string SearchCondition => $" WHERE SifraLajsne={Id}";

        public string ColumnNames => "SifraLajsne,NazivLajsne,UrlSlike,NazivTipa,SifraTipa,TrenutnaCena";

        public string SetColumnValues => $" NazivLajsne='{NazivLajsne}',UrlSlike='{UrlSlike}',SifraTipa={TipLajsne.Id}";

        public string JoinSelect => throw new NotImplementedException();

        public string JoinTables => throw new NotImplementedException();

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> lajsne = new List<IDomainObject>();
                while (reader.Read())
                {
                    Lajsna lajsna = new Lajsna
                    {
                        Id = (long)reader[0],
                        NazivLajsne = reader[1].ToString(),
                        NazivTipa = reader[3].ToString(),
                        TipLajsne = new TipLajsne() { Id = (long)reader[4] }
                    };
                    if (reader[2] == DBNull.Value) lajsna.UrlSlike = null;
                    else lajsna.UrlSlike = reader[2].ToString();
                    if (reader[5] == DBNull.Value) lajsna.TrenutnaCena = null;
                    else lajsna.TrenutnaCena = (int)reader[5];
                    lajsne.Add(lajsna);
                }
                Debug.WriteLine("Gotov sa listom");
                return lajsne;
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
                Lajsna lajsna = new Lajsna();
                while (reader.Read())
                {
                    lajsna.Id = (long)reader[0];
                    lajsna.NazivLajsne = reader[1].ToString();
                    if (reader[2] == DBNull.Value) lajsna.UrlSlike = null;
                    else lajsna.UrlSlike = reader[2].ToString();
                    lajsna.NazivTipa = reader[3].ToString();
                    lajsna.TipLajsne = new TipLajsne() { Id = (long)reader[4] };
                    if (reader[5] == DBNull.Value) lajsna.TrenutnaCena = null;
                    else lajsna.TrenutnaCena = (int)reader[5];
                    break;
                }
                return lajsna;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}