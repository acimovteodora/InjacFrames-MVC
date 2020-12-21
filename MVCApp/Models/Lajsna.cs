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
        public long Id { get; set; }
        [DisplayName("Naziv lajsne")]
        public string NazivLajsne { get; set; }
        [DisplayName("Naziv tipa lajsne")]
        public string NazivTipa { get; set; }
        public string UrlSlike { get; set; }
        [DisplayName("Aktuelna cena(metar)")]
        public int TrenutnaCena { get; set; }
        [DisplayName("Sifra tipa lajsne")]
        public TipLajsne TipLajsne { get; set; }

        public string TabelName => "Lajsna";

        public string InsertValue => $" {NazivLajsne},{UrlSlike},{NazivTipa},{TipLajsne.Id},{TrenutnaCena}";

        public string SearchCondition => $" WHERE SifraLajsne={Id}";

        public string ColumnNames => "NazivLajsne,UrlSlike,NazivTipa,SifraTipa,TrenutnaCena";

        public string SetColumnValues => $" NazivLajsne='{NazivLajsne}',UrlSlike='{UrlSlike}',SifraTipa={TipLajsne.Id}";

        public string IdColumn => "SifraLajsne";

        int IDomainObject.Id => 0;

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> lajsne = new List<IDomainObject>();
                while (reader.Read())
                {
                    Lajsna lajsna = new Lajsna();
                    lajsna.Id = (long)reader[0];
                    lajsna.NazivLajsne = reader[1].ToString();
                    lajsna.UrlSlike = reader[2].ToString();
                    lajsna.NazivTipa = reader[3].ToString();
                    lajsna.TipLajsne = new TipLajsne() { Id = (long)reader[4] };
                    lajsna.TrenutnaCena = (int)reader[5];
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
                    lajsna.UrlSlike = reader[2].ToString();
                    lajsna.NazivTipa = reader[3].ToString();
                    lajsna.TipLajsne = new TipLajsne() { Id = (long)reader[4] };
                    lajsna.TrenutnaCena = (int)reader[5];
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