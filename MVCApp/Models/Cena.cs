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

        public string InsertValue => $" {Lajsna.Id},{DatumOd},{DatumDo},{Iznos}";

        public string SearchCondition => $" WHERE SifraLajsne={Lajsna.Id} AND DatumOd ='{DatumOd}'";

        public string ColumnNames => "SifraLajsne,DatumOd,DatumDo,Iznos";

        public string SetColumnValues => $" SifraLajsne='{Lajsna.Id}',DatumOd='{DatumOd}',DatumDo='{DatumDo}',Iznos={Iznos}";

        public string IdColumn => "DatumOd";

        public int Id => 0;

        public List<IDomainObject> VratiListu(SqlDataReader reader)
        {
            try
            {
                List<IDomainObject> cene = new List<IDomainObject>();
                while (reader.Read())
                {
                    Cena cena = new Cena();
                    cena.Lajsna = new Lajsna() { Id = (long)reader[0] };
                    cena.DatumOd = Convert.ToDateTime(reader[1].ToString());
                    if (!string.IsNullOrEmpty(reader[2].ToString()))
                        cena.DatumDo = Convert.ToDateTime(reader[2].ToString());
                    else cena.DatumDo = null;
                    //cena.DatumDo = reader[2] == DBNull.Value ? null : Convert.ToDateTime(reader[2].ToString());
                    cena.Iznos = (int)reader[3];
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
            throw new NotImplementedException();
        }
    }
}