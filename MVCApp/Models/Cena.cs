using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Cena : IDomainObject
    {
        public Lajsna Lajsna { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public int Iznos { get; set; }

        public string TabelName => "Cena";

        public string InsertValue => $"  VALUES({Lajsna.Id},'{DatumOd}',NULL,{Iznos})";

        public string SearchCondition => $" WHERE SifraLajsne={Lajsna.Id} AND DatumOd ='{DatumOd}'";

        public string ColumnNames => "SifraLajsne,DatumOd,DatumDo,Iznos";

        public string SetColumnValues => $" SifraLajsne='{Lajsna.Id}',DatumOd='{DatumOd}',DatumDo=NULL,Iznos={Iznos}";

        public string InsertColumns => "SifraLajsne,DatumOd,DatumDo,Iznos";

        public string JoinSelect => "c.DatumOd, c.DatumDo, c.Iznos, c.SifraLajsne, l.NazivLajsne";

        public string JoinTables => "from Cena c join Lajsna l on c.SifraLajsne = l.SifraLajsne";

        public string Identifikator => "";

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> cene = new List<IDomainObject>();
                while (reader.Read())
                {
                    Cena cena = new Cena
                    {
                        Lajsna = new Lajsna() { Id = (long)reader[3], NazivLajsne = reader[4].ToString() },
                        DatumOd = (DateTime)reader[0],
                        Iznos = (int)reader[2]
                    };
                    if (!string.IsNullOrEmpty(reader[1].ToString()))
                        cena.DatumDo = (DateTime)reader[1];
                    else cena.DatumDo = null;
                    //cena.DatumDo = reader[2] == DBNull.Value ? null : Convert.ToDateTime(reader[2].ToString());
                    cene.Add(cena);
                }
                return cene;
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
                Cena cena = new Cena();
                while (reader.Read())
                {
                    cena.Lajsna = new Lajsna() { Id = (long)reader[3], NazivLajsne = reader[4].ToString() };
                    cena.DatumOd = (DateTime)reader[0];
                    cena.Iznos = (int)reader[2];
                    if (!string.IsNullOrEmpty(reader[1].ToString()))
                        cena.DatumDo = (DateTime)reader[1];
                    else cena.DatumDo = null;
                    //cena.DatumDo = reader[2] == DBNull.Value ? null : Convert.ToDateTime(reader[2].ToString());
                    break;
                }
                return cena;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}