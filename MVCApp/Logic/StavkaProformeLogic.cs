using MVCApp.DatabaseBroker;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Logic.Interfaces
{
    public class StavkaProformeLogic : IStavkaProformeLogic
    {
        private readonly Broker _broker;
        public StavkaProformeLogic(Broker broker)
        {
            _broker = broker;
        }
        public bool CreateObject(StavkaProforme objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                objekat.Lajsna = _broker.ReturnByCriteria($"WHERE NazivLajsne LIKE '{objekat.Lajsna.NazivLajsne}'", objekat.Lajsna).OfType<Lajsna>().ToList().FirstOrDefault();
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

        public bool DeleteObject(StavkaProforme objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                _broker.DeleteObject(objekat);
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

        public StavkaProforme SelectObject(StavkaProforme objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                StavkaProforme stavka = _broker.SelectObjectJoin(objekat) as StavkaProforme;
                _broker.Commit();
                return stavka;
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

        public bool UpdateObject(StavkaProforme objekat)
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
