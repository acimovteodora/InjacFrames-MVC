using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class NalogZaUtovar:IDomainObject
    {
        [DisplayName("Šifra naloga")]
        public long Id { get; set; }
        [DisplayName("Broj paleta")]
        public int BrojPaleta { get; set; }
        [DisplayName("Dimenzije palete")]
        public string DimenzijePalete { get; set; }
        [DisplayName("Neto težina palete")]
        public double NetoTezinaPalete { get; set; }
        [DisplayName("Vreme utovara")]
        public DateTime DatumVremeUtovara { get; set; }
        [DisplayName("Cena transporta")]
        public int CenaTransporta { get; set; }
        public string Napomena { get; set; }
        [DisplayName("Izvoz")]
        public Carinik CarinikIzvoz { get; set; }
        [DisplayName("Uvoz")]
        public Carinik CarinikUvoz { get; set; }
        public Prevoznik Prevoznik { get; set; }
        public Porudzbina Porudzbina { get; set; }

        public string TabelName => "NalogZaUtovar_View";

        public string InsertColumns => "SifraNaloga,CenaTransporta,DatumVremeUtovara,BrojPaleta,DimenzijePalete,NetoTezinaPalete,Napomena," +
                                     "SifraPrevoznika,SifraIzvozCarinik,SifraUvozCarinik,SifraPorudzbine";

        public string InsertValue => $" VALUES(0,{CenaTransporta},'{DatumVremeUtovara}',{BrojPaleta},'{DimenzijePalete}',{NetoTezinaPalete},"+
                                     $"'{Napomena}',{Prevoznik.Id},{CarinikIzvoz.Id},{CarinikUvoz.Id},{Porudzbina.Id})";

        public string ColumnNames => "SifraNaloga,CenaTransporta,DatumVremeUtovara,BrojPaleta,DimenzijePalete,NetoTezinaPalete,Napomena,"+
                                     "SifraPrevoznika,SifraIzvozCarinik,SifraUvozCarinik,SifraPorudzbine";

        public string SetColumnValues => $"SifraNaloga={Id},CenaTransporta={CenaTransporta},DatumVremeUtovara='{DatumVremeUtovara}',"+
                                         $"BrojPaleta={BrojPaleta},DimenzijePalete='{DimenzijePalete}',NetoTezinaPalete={NetoTezinaPalete},"+
                                         $"Napomena='{Napomena}',SifraPrevoznika={Prevoznik.Id},SifraIzvozCarinik={CarinikIzvoz.Id},"+
                                         $"SifraUvozCarinik={CarinikUvoz.Id},SifraPorudzbine={Porudzbina.Id}";

        public string SearchCondition => $" WHERE SifraNaloga={Id}";

        public string JoinSelect => "n.SifraNaloga, n.CenaTransporta, n.DatumVremeUtovara, n.BrojPaleta, n.DimenzijePalete, n.NetoTezinaPalete, n.Napomena, "+
                                    "por.SifraPorudzbine, por.Datum, "+
                                    "k.NazivKompanije, i.NazivKompanije, u.NazivKompanije, p.NazivKompanije,"+
                                    "k.SifraKompanije, i.SifraKompanije, u.SifraKompanije, p.SifraKompanije";

        public string JoinTables => "FROM NalogZaUtovar_View n JOIN Porudzbina por ON n.SifraPorudzbine = por.SifraPorudzbine "+
                                    "JOIN Kompanija k ON por.SifraKompanije = k.SifraKompanije "+
                                    "JOIN Kompanija i ON n.SifraIzvozCarinik = i.SifraKompanije "+
                                    "JOIN Kompanija u ON n.SifraUvozCarinik = u.SifraKompanije "+
                                    "JOIN Kompanija p ON n.SifraPrevoznika = p.SifraKompanije";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> nalozi = new List<IDomainObject>();
                while (reader.Read())
                {
                    NalogZaUtovar nalog = new NalogZaUtovar
                    {
                        Id = (long)reader[0],
                        CenaTransporta = (int)reader[1],
                        DatumVremeUtovara = (DateTime)reader[2],
                        BrojPaleta = (int)reader[3],
                        DimenzijePalete = reader[4].ToString(),
                        NetoTezinaPalete = (double)reader[5],
                        Porudzbina = new Porudzbina() { Id = (long)reader[7], Datum = (DateTime)reader[8], Klijent = new Klijent() { NazivKompanije = reader[9].ToString()} },
                        CarinikIzvoz = new Carinik() { NazivKompanije = reader[10].ToString() },
                        CarinikUvoz = new Carinik() { NazivKompanije = reader[11].ToString() },
                        Prevoznik = new Prevoznik() { NazivKompanije = reader[12].ToString() }
                    };
                    if (reader[6] == DBNull.Value) nalog.Napomena = null;
                    else nalog.Napomena = reader[6].ToString();
                    nalozi.Add(nalog);
                }
                return nalozi;
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
                NalogZaUtovar nalog = new NalogZaUtovar();
                while (reader.Read())
                {
                    nalog.Id = (long)reader[0];
                    nalog.CenaTransporta = (int)reader[1];
                    nalog.DatumVremeUtovara = (DateTime)reader[2];
                    nalog.BrojPaleta = (int)reader[3];
                    nalog.DimenzijePalete = reader[4].ToString();
                    nalog.NetoTezinaPalete = (double)reader[5];
                    nalog.Porudzbina = new Porudzbina() { Id = (long)reader[7], Datum = (DateTime)reader[8], Klijent = new Klijent() { Id = (long)reader[13], NazivKompanije = reader[9].ToString() } };
                    nalog.CarinikIzvoz = new Carinik() { Id = (long)reader[14], NazivKompanije = reader[10].ToString() };
                    nalog.CarinikUvoz = new Carinik() { Id = (long)reader[15], NazivKompanije = reader[11].ToString() };
                    nalog.Prevoznik = new Prevoznik() { Id = (long)reader[16], NazivKompanije = reader[12].ToString() };
                    if (reader[6] == DBNull.Value) nalog.Napomena = null;
                    else nalog.Napomena = reader[6].ToString();
                    break;
                }
                return nalog;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [DisplayName("Datum utovara")]
        public string DatumUtovara => DatumVremeUtovara.ToString("dd.MM.yyyy.");

        public string Identifikator => "";
    }
}