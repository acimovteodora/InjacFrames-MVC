using MVCApp.DataAccessLayer.Interfaces;
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
    public class NalogLogic : INalogLogic
    {
        private readonly Broker _broker;
        public NalogLogic(Broker broker)
        {
            _broker = broker;
        }

        public bool CreateObject(NalogZaUtovar objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                objekat.Prevoznik = _broker.ReturnByCriteriaJoin($" WHERE NazivKompanije LIKE '{objekat.Prevoznik.NazivKompanije}'", objekat.Prevoznik).OfType<Prevoznik>().ToList().FirstOrDefault();
                objekat.CarinikIzvoz = _broker.ReturnByCriteriaJoin($" WHERE NazivKompanije LIKE '{objekat.CarinikIzvoz.NazivKompanije}'", objekat.CarinikIzvoz).OfType<Carinik>().ToList().FirstOrDefault();
                objekat.CarinikUvoz = _broker.ReturnByCriteriaJoin($" WHERE NazivKompanije LIKE '{objekat.CarinikUvoz.NazivKompanije}'", objekat.CarinikUvoz).OfType<Carinik>().ToList().FirstOrDefault();
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

        public bool DeleteObject(NalogZaUtovar objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                NalogZaUtovar nalog = _broker.SelectObject(objekat) as NalogZaUtovar;
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

        public List<Carinik> GetCarinici()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Carinik> companies = _broker.SelectAllJoin(new Carinik()).OfType<Carinik>().ToList();
                _broker.Commit();
                return companies;
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

        public List<Porudzbina> GetPorudzbineInsert()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Porudzbina> porudzbine = _broker.ReturnByCriteriaJoin("WHERE SifraPorudzbine NOT IN (SELECT SifraPorudzbine FROM NalogZaUtovar_View)",new Porudzbina()).OfType<Porudzbina>().ToList();
                _broker.Commit();
                return porudzbine;
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
        
        public List<Porudzbina> GetPorudzbineUpdate()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Porudzbina> porudzbine = _broker.ReturnByCriteriaJoin("WHERE SifraPorudzbine IN (SELECT SifraPorudzbine FROM NalogZaUtovar_View)",new Porudzbina()).OfType<Porudzbina>().ToList();
                _broker.Commit();
                return porudzbine;
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

        public List<Prevoznik> GetPrevoznici()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Prevoznik> companies = _broker.SelectAllJoin(new Prevoznik()).OfType<Prevoznik>().ToList();
                _broker.Commit();
                return companies;
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

        public List<NalogZaUtovar> SelectAll(NalogZaUtovar objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<NalogZaUtovar> list = _broker.SelectAllJoin(objekat).OfType<NalogZaUtovar>().ToList();
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

        public NalogZaUtovar SelectObject(NalogZaUtovar objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                NalogZaUtovar nalog = _broker.SelectObjectJoin(objekat) as NalogZaUtovar;
                _broker.Commit();
                return nalog;
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

        public bool UpdateObject(NalogZaUtovar objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                if (!_broker.UpdateObject(objekat)) throw new Exception();
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
