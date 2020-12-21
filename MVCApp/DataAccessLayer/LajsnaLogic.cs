using MVCApp.DatabaseBroker;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DataAccessLayer
{
    public class LajsnaLogic : ILajsnaLogic
    {
        private readonly Broker _broker;
        public LajsnaLogic(Broker broker)
        {
            _broker = broker;
        }
        public int DeleteObject(Lajsna objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Lajsna lajsna = _broker.ReturnObject(objekat) as Lajsna;
                List<Cena> cene = _broker.ReturnByCriteria($"WHERE SifraLajsne={lajsna.Id}", new Cena()).OfType<Cena>().ToList();

                foreach (var cena in cene)
                {
                     _broker.DeleteObject(new Cena() { Lajsna = new Lajsna() { Id = objekat.Id }, DatumOd = cena.DatumOd });
                }
                _broker.DeleteObject(objekat);
                _broker.Commit();
                return 1;
            }
            catch(SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                //_broker.Rollback();
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public List<Lajsna> ReturnAll(Lajsna objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Lajsna> list = _broker.ReturnAll(objekat).OfType<Lajsna>().ToList();
                _broker.Commit();
                return list;
            }
            catch (SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public List<Lajsna> ReturnByCriteria(string criteria, Lajsna objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Lajsna> lajsne = _broker.ReturnByCriteria(criteria,objekat).OfType<Lajsna>().ToList();
                _broker.Commit();
                return lajsne;
            }
            catch (SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public Lajsna ReturnObject(Lajsna objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Lajsna lajsna = _broker.ReturnObject(objekat) as Lajsna;
                _broker.Commit();
                return lajsna;
            }
            catch (SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public List<TipLajsne> GetTipoviLajsni()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<TipLajsne> types =_broker.ReturnAll(new TipLajsne()).OfType<TipLajsne>().ToList();
                _broker.Commit();
                return types;
            }
            catch (SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public bool UpdateObject(Lajsna objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Lajsna fromDb = _broker.ReturnObject(objekat) as Lajsna;
                string values = null;
                if(fromDb.TrenutnaCena != objekat.TrenutnaCena && fromDb.NazivTipa != objekat.NazivTipa)
                {
                    values = $"TrenutnaCena = {objekat.TrenutnaCena},NazivTipa = {objekat.NazivTipa}";
                } else if(fromDb.NazivTipa != objekat.NazivTipa)
                {
                    values = $"NazivTipa = {objekat.NazivTipa}";
                } else if(fromDb.TrenutnaCena != objekat.TrenutnaCena)
                {
                    values = $"TrenutnaCena = {objekat.TrenutnaCena}";
                }
                if (string.IsNullOrEmpty(values))
                {
                    if (!_broker.UpdateObject(objekat)) throw new Exception();
                }
                else if(!_broker.UpdateSpecific(objekat, values)) throw new Exception();
                _broker.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.Write(">>>>>>>> " + ex.Message);
                _broker.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                _broker.Rollback();
                return false;
            }
            finally
            {
                _broker.CloseConnection();
            }
        }
    }
}
