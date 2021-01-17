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
    public class GradLogic : IGradLogic
    {
        private readonly Broker _broker;
        public GradLogic(Broker broker)
        {
            _broker = broker;
        }

        public List<Drzava> GetCountries()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Drzava> list = _broker.SelectAll(new Drzava()).OfType<Drzava>().ToList();
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

        public List<Grad> SelectAll(Grad objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Grad> list = _broker.SelectAllJoin(objekat).OfType<Grad>().ToList();
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

        public Grad SelectObject(Grad objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Grad grad = _broker.SelectObject(objekat) as Grad;
                _broker.Commit();
                return grad;
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

        public bool UpdateObject(Grad objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Grad fromDb = _broker.SelectObject(objekat) as Grad;
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
