using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly
{
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined,
    MaxByteSize = 800)]
    public struct Dimenzije : INullable, IBinarySerialize
    {
        public bool IsNull
        {
            get
            {
                return m_Null;
            }
        }
        public static Dimenzije Null
        {
            get
            {
                Dimenzije h = new Dimenzije();
                h.m_Null = true;
                return h;
            }
        }
        private int duzina;
        private int visina;
        private bool m_Null;
        public Dimenzije(int duzina, int visina)
        {
            this.duzina = duzina;
            this.visina = visina;
            m_Null = false;
        }
        public override string ToString()
        {
            if (this.IsNull)
                return "null";
            else
            {
                string delim = new string((new char[] { ';' }));
                return (this.duzina + delim + this.visina);
            }
        }
        public static Dimenzije Parse(SqlString s)
        {
            if (s.IsNull)
                return Null;
            else
            {
                Dimenzije dimenzije = new Dimenzije();
                string str = Convert.ToString(s);
                string[] a = null;
                a = str.Split(new char[] { ';' });
                int d1 = Convert.ToInt32(a[0]);
                int v1 =  Convert.ToInt32(a[1]);
                ValidateBroj(d1);
                ValidateBroj(v1);
                dimenzije.duzina = d1;
                dimenzije.visina = v1;
                dimenzije.m_Null = false;
                return (dimenzije);
            }
        }
        private static void ValidateBroj(int broj)
        {
            if (broj < 1)
            {
                throw new ArgumentOutOfRangeException("Broj ne može biti manji od 1");
            }
        }
        public int Duzina
        {
            get
            {
                return (this.duzina);
            }
            set
            {
                this.duzina = value;
                this.m_Null = false;
            }
        }
        public int Visina
        {
            get
            {
                return (this.visina);
            }
            set
            {
                this.visina = value;
                this.m_Null = false;
            }
        }
        public override bool Equals(object other)
        {
            return this.CompareTo(other) == 0;
        }
        public override int GetHashCode()
        {
            if (this.IsNull)
                return 0;
            return this.ToString().GetHashCode();
        }
        public int CompareTo(object other)
        {
            if (other == null)
                return 1; //by definition
            Dimenzije dimenzije = (Dimenzije)other;
            if (dimenzije.Equals(null))
                throw new ArgumentException("the argument to compare is not a dimenzija");
            if (this.IsNull)
            {
                if (dimenzije.IsNull)
                    return 0;
                return -1;
            }
            if (dimenzije.IsNull)
                return 1;
            return this.ToString().CompareTo(dimenzije.ToString());
        }
        public void Write(System.IO.BinaryWriter w)
        {
            byte header = (byte)(this.IsNull ? 1 : 0);
            w.Write(header);
            if (header == 1)
            {
                return;
            }
            w.Write(this.Duzina);
            w.Write(this.Visina);
        }
        public void Read(System.IO.BinaryReader r)
        {
            byte header = r.ReadByte();
            if (header == 1)
            {
                this.m_Null = true;
                return;
            }
            this.m_Null = false;
            this.duzina = r.ReadInt32();
            this.visina = r.ReadInt32();
        }
    }
}
