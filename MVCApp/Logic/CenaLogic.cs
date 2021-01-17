using MVCApp.Logic.Interfaces;
using MVCApp.DatabaseBroker;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic
{
    public class CenaLogic : ICenaLogic
    {
        private readonly Broker _broker;
        public CenaLogic(Broker broker)
        {
            _broker = broker;
        }
        public bool CreateObject(Cena objekat)
        {
            try
            {
                objekat.DatumDo = null;
                _broker.OpenConnection();
                _broker.BeginTransaction();
                _broker.InsertObject(objekat);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public bool DeleteObject(Cena objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                _broker.DeleteObjectCriteria($"WHERE SifraLajsne={objekat.Lajsna.Id} and DatumDo is null",objekat);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public List<Lajsna> GetLajsne()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Lajsna> list = _broker.SelectAll(new Lajsna()).OfType<Lajsna>().ToList();
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

        public List<Cena> SelectAll(Cena objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Cena> cene = _broker.ReturnByCriteriaJoin("WHERE DatumDo is null", objekat).OfType<Cena>().ToList();
                _broker.Commit();
                return cene;
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

        public Cena SelectObject(Cena objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Cena cene = _broker.ReturnByCriteriaJoin($"WHERE c.SifraLajsne={objekat.Lajsna.Id} and c.DatumDo is null", objekat).OfType<Cena>().ToList().FirstOrDefault();
                _broker.Commit();
                return cene;
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

        public bool UpdateObject(Cena objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                _broker.UpdateObject(objekat);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                _broker.CloseConnection();
            }
        }
    }
}
