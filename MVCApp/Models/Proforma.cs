using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Proforma : IDomainObject
    {
        public long Id { get; set; }
        public DateTime Datum { get; set; }
        public int? Popust { get; set; }
        public double? UkupnaCena { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public NacinIsporuke NacinIsporuke { get; set; }
        public Racun Racun { get; set; }
        public IList<StavkaProforme> Stavke { get; set; }

        public string Identifikator => "SifraProforme";

        public string TabelName => "Proforma";

        public string InsertColumns => "Datum,Popust,SifraZaposlenog,SifraPorudzbine,SifraBanke,SifraRacuna,SifraIsporuke";

        public string InsertValue => $"'VALUES ({Datum}',{Popust},{Zaposleni.Id},{Porudzbina.Id},{Racun.Banka.Id},{Racun.Id},{NacinIsporuke.Id})";

        public string ColumnNames => "SifraProforme,Datum,Popust,UkupnaCena,SifraZaposlenog,SifraPorudzbine,SifraBanke,SifraRacuna,SifraIsporuke";

        public string SetColumnValues => $"Datum='{Datum}',Popust={Popust},SifraZaposlenog={Zaposleni.Id},SifraPorudzbine={Porudzbina.Id},SifraBanke={Racun.Banka.Id},SifraRacuna={Racun.Id},SifraIsporuke={NacinIsporuke.Id}";

        public string SearchCondition => $"WHERE SifraProforme={Id}";

        public string JoinSelect => "p.SifraProforme, p.Datum, p.Popust, p.UkupnaCena, p.SifraZaposlenog, p.SifraPorudzbine, p.SifraRacuna, r.BrojRacuna, b.SifraBanke, p.SifraIsporuke, n.NazivIsporuke";

        public string JoinTables => "from Proforma p join Racun r on p.SifraRacuna=r.SifraRacuna join Banka b on r.SifraBanke=b.SifraBanke"
                                    +" join NacinIsporuke n on p.SifraIsporuke=n.SifraIsporuke";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> proforme = new List<IDomainObject>();
                while (reader.Read())
                {
                    Debug.WriteLine(reader[7].ToString());
                    Proforma proforma = new Proforma
                    {
                        Id = (long)reader[0],
                        Datum = (DateTime)reader[1],
                        Zaposleni = new Zaposleni() { Id = (long)reader[4] },
                        Porudzbina = new Porudzbina() { Id = (long)reader[5] },
                        Racun = new Racun() { Id = (long)reader[6], Vrednost = reader[7].ToString(), Banka = new Banka() { Id = (long)reader[8] } },
                        NacinIsporuke = new NacinIsporuke() { Id = (long)reader[9], NazivIsporuke = reader[10].ToString() }
                    };
                    if (reader[2] == DBNull.Value) proforma.Popust = null;
                    else proforma.Popust = (int)reader[2];
                    if (reader[3] == DBNull.Value) proforma.UkupnaCena = null;
                    else proforma.UkupnaCena = (double)reader[3];
                    proforme.Add(proforma);
                }
                return proforme;
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
                Proforma proforma = new Proforma();
                while (reader.Read())
                {
                    proforma.Id = (long)reader[0];
                    proforma.Datum = (DateTime)reader[1];
                    proforma.Zaposleni = new Zaposleni() { Id = (long)reader[4] };
                    proforma.Porudzbina = new Porudzbina() { Id = (long)reader[5] };
                    proforma.Racun = new Racun() { Id = (long)reader[6], Vrednost = reader[7].ToString(), Banka = new Banka() { Id = (long)reader[8] } };
                    proforma.NacinIsporuke = new NacinIsporuke() { Id = (long)reader[9], NazivIsporuke = reader[10].ToString() };
                    if (reader[2] == DBNull.Value) proforma.Popust = null;
                    else proforma.Popust = (int)reader[2];
                    if (reader[3] == DBNull.Value) proforma.UkupnaCena = null;
                    else proforma.UkupnaCena = (double)reader[3];
                    break;
                }
                return proforma;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public StavkaProforme NovaStavka { get; set; }
    }
}