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
    public class AdresaLogic : IAdresaLogic
    {
        private readonly Broker _broker;
        public AdresaLogic(Broker broker)
        {
            _broker = broker;
        }

        public bool CreateObject(Adresa objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                objekat.Kompanija = _broker.ReturnByCriteria($"WHERE NazivKompanije LIKE '{objekat.Kompanija.NazivKompanije}'", objekat.Kompanija).OfType<Kompanija>().ToList().FirstOrDefault();
                objekat.Grad = _broker.ReturnByCriteriaJoin($"WHERE NazivGrada LIKE '{objekat.Grad.NazivGrada}'", objekat.Grad).OfType<Grad>().ToList().FirstOrDefault();
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

        public List<Grad> GetCities()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Grad> list = _broker.SelectAllJoin(new Grad()).OfType<Grad>().ToList();
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

        public List<Kompanija> GetCompanies()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Kompanija> list = _broker.SelectAll(new Kompanija()).OfType<Kompanija>().ToList();
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

        public List<Adresa> SelectAll(Adresa objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Adresa> list = _broker.SelectAllJoin(objekat).OfType<Adresa>().ToList();
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

        public Adresa SelectObject(Adresa objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Adresa adresa = _broker.SelectObject(objekat) as Adresa;
                _broker.Commit();
                return adresa;
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

        public bool UpdateObject(Adresa objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Adresa fromDb = _broker.SelectObject(objekat) as Adresa;
                string values = null;
                if (fromDb.NazivGrada != objekat.NazivGrada && fromDb.Grad.Id == objekat.Grad.Id)
                {
                    values = $"NazivGrada = '{objekat.NazivGrada}'";
                }
                if (string.IsNullOrEmpty(values))
                {
                    if (!_broker.UpdateObject(objekat)) throw new Exception();
                }
                else if (!_broker.UpdateSpecific(objekat, values)) throw new Exception();
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
