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
    public class TipLajsneLogic : ITipLajsneLogic
    {
        private readonly Broker _broker;
        public TipLajsneLogic(Broker broker)
        {
            _broker = broker;
        }

        public bool CreateObject(TipLajsne objekat)
        {
            try
            {
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

        public List<TipLajsne> SelectAll(TipLajsne objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<TipLajsne> list = _broker.SelectAll(objekat).OfType<TipLajsne>().ToList();
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

        public TipLajsne SelectObject(TipLajsne objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                TipLajsne tipLajsne = _broker.SelectObject(objekat) as TipLajsne;
                _broker.Commit();
                return tipLajsne;
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

        public bool UpdateObject(TipLajsne objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                TipLajsne fromDb = _broker.SelectObject(objekat) as TipLajsne;
                if (!_broker.UpdateObject(objekat))
                {
                    throw new Exception();
                }
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
