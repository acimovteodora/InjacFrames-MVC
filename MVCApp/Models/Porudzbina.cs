using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Porudzbina : IDomainObject
    {
        [DisplayName("Šifra porudžbine")]
        public long Id { get; set; }
        [DisplayName("Vreme kreiranja")]
        public DateTime Datum { get; set; }
        [DisplayName("Uslovi isporuke")]
        public string UsloviIsporuke { get; set; }
        public Klijent Klijent { get; set; }
        public Katalog Katalog { get; set; }
        [DisplayName("Način isporuke")]
        public NacinIsporuke NacinIsporuke { get; set; }
        [DisplayName("Način plaćanja")]
        public NacinPlacanja NacinPlacanja { get; set; }
        public IList<StavkaPorudzbine> Stavke { get; set; }

        public string TabelName => "Porudzbina";

        public string InsertColumns => "Datum,UsloviIsporuke,SifraKataloga,SifraKompanije,SifraPlacanja,SifraIsporuke";

        public string InsertValue => $"SELECT '{Datum}', '{UsloviIsporuke}', {Katalog.Id}, {Klijent.Id},{NacinPlacanja.Id},{NacinIsporuke.Id}";

        public string ColumnNames => "SifraPorudzbine,Datum,UsloviIsporuke,SifraKataloga,SifraKompanije,SifraPlacanja,SifraIsporuke";

        public string SetColumnValues => $"Datum='{Datum}',UsloviIsporuke='{UsloviIsporuke}',SifraKataloga={Katalog.Id},SifraKompanije={Klijent.Id},SifraPlacanja={NacinPlacanja.Id},SifraIsporuke={NacinIsporuke.Id}";

        public string SearchCondition => $"WHERE SifraPorudzbine={Id}";

        public string JoinSelect => "p.SifraPorudzbine, p.Datum, p.UsloviIsporuke, p.SifraKompanije, k.NazivKompanije, p.SifraIsporuke, ni.NazivIsporuke, p.SifraPlacanja, np.NazivPlacanja, p.SifraKataloga";

        public string JoinTables => "FROM Porudzbina p JOIN Kompanija k ON p.SifraKompanije = k.SifraKompanije "+
                                    "JOIN NacinIsporuke ni ON p.SifraIsporuke = ni.SifraIsporuke "+
                                    "JOIN NacinPlacanja np ON p.SifraPlacanja = np.SifraPlacanja";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> porudzbine = new List<IDomainObject>();
                while (reader.Read())
                {
                    Porudzbina porudzbina = new Porudzbina
                    {
                        Id = (long)reader[0],
                        Datum = (DateTime)reader[1],
                        Klijent = new Klijent() { Id = (long)reader[3], NazivKompanije = reader[4].ToString() },
                        NacinIsporuke = new NacinIsporuke() { Id = (long)reader[5], NazivIsporuke = reader[6].ToString() },
                        NacinPlacanja = new NacinPlacanja() { Id = (long)reader[7], NazivPlacanja = reader[8].ToString() },
                        Katalog = new Katalog() { Id = (long)reader[9] }
                    };
                    if (reader[2] == DBNull.Value) porudzbina.UsloviIsporuke = null;
                    else porudzbina.UsloviIsporuke = reader[2].ToString();
                    porudzbine.Add(porudzbina);
                }
                return porudzbine;
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
                Porudzbina porudzbina = new Porudzbina();
                while (reader.Read())
                {
                    porudzbina.Id = (long)reader[0];
                    porudzbina.Datum = (DateTime)reader[1];
                    porudzbina.Klijent = new Klijent() { Id = (long)reader[3], NazivKompanije = reader[4].ToString() };
                    porudzbina.NacinIsporuke = new NacinIsporuke() { Id = (long)reader[5], NazivIsporuke = reader[6].ToString() };
                    porudzbina.NacinPlacanja = new NacinPlacanja() { Id = (long)reader[7], NazivPlacanja = reader[8].ToString() };
                    porudzbina.Katalog = new Katalog() { Id = (long)reader[9] };
                    if (reader[2] == DBNull.Value) porudzbina.UsloviIsporuke = null;
                    else porudzbina.UsloviIsporuke = reader[2].ToString();
                    break;
                }
                return porudzbina;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [DisplayName("Datum porudžbine")]
        public string DatumPoruzbine => Datum.ToString("dd.MM.yyyy.");
        public StavkaPorudzbine NovaStavka { get; set; }
        public string Identifikator => "OUTPUT inserted.SifraPorudzbine";
        public string Button { get; set; }
    }
}