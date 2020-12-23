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
    public class PorudzbinaLogic : IPorudzbinaLogic
    {
        private readonly Broker _broker;
        public PorudzbinaLogic(Broker broker)
        {
            _broker = broker;
        }
        public bool CreateObject(Porudzbina objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                objekat.Datum = DateTime.Now;
                objekat.Katalog = _broker.ReturnByCriteria($"WHERE Godina={DateTime.Today.Year}", new Katalog()).OfType<Katalog>().ToList().FirstOrDefault();
                objekat.Klijent = _broker.ReturnByCriteriaJoin($" WHERE NazivKompanije LIKE '{objekat.Klijent.NazivKompanije}'", objekat.Klijent).OfType<Klijent>().ToList().FirstOrDefault();
                objekat.NacinIsporuke = _broker.ReturnByCriteria($" WHERE NazivIsporuke LIKE '{objekat.NacinIsporuke.NazivIsporuke}'", objekat.NacinIsporuke).OfType<NacinIsporuke>().ToList().FirstOrDefault();
                objekat.NacinPlacanja = _broker.ReturnByCriteria($" WHERE NazivPlacanja LIKE '{objekat.NacinPlacanja.NazivPlacanja}'", objekat.NacinPlacanja).OfType<NacinPlacanja>().ToList().FirstOrDefault();
                long id =_broker.InsertObject(objekat);
                foreach (var item in objekat.Stavke)
                {
                    item.Porudzbina = new Porudzbina() { Id = id };
                    item.Lajsna = _broker.ReturnByCriteria($"WHERE NazivLajsne LIKE '{item.Lajsna.NazivLajsne}'", new Lajsna()).OfType<Lajsna>().ToList().FirstOrDefault();
                    _broker.InsertObject(item);
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

        public Katalog GetKatalog(string criteria)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Katalog objekat = _broker.ReturnByCriteria(criteria, new Katalog()).OfType<Katalog>().ToList().FirstOrDefault();
                _broker.Commit();
                return objekat;
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

        public List<Klijent> GetKlijenti()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Klijent> list = _broker.SelectAllJoin(new Klijent()).OfType<Klijent>().ToList();
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
        public Lajsna GetLajsna(string name)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Lajsna lajsna = _broker.ReturnByCriteria($"WHERE NazivLajsne LIKE '{name}'",new Lajsna()).OfType<Lajsna>().ToList().FirstOrDefault();
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
        public List<Lajsna> GetLajsne()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Lajsna> list = _broker.SelectAll(new Lajsna()).OfType<Lajsna>().ToList();
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

        public List<NacinIsporuke> GetNaciniIsporuke()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<NacinIsporuke> list = _broker.SelectAll(new NacinIsporuke()).OfType<NacinIsporuke>().ToList();
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

        public List<NacinPlacanja> GetNaciniPlacanja()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<NacinPlacanja> list = _broker.SelectAll(new NacinPlacanja()).OfType<NacinPlacanja>().ToList();
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

        public List<Porudzbina> SelectAll(Porudzbina objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Porudzbina> list = _broker.SelectAllJoin(objekat).OfType<Porudzbina>().ToList();
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

        public List<Porudzbina> SelectByCriteria(string criteria, Porudzbina objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Porudzbina> porudzbine = _broker.ReturnByCriteriaJoin(criteria, objekat).OfType<Porudzbina>().ToList();
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

        public Porudzbina SelectObject(Porudzbina objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Porudzbina porudzbina = _broker.SelectObjectJoin(objekat) as Porudzbina;
                porudzbina.Stavke = _broker.ReturnByCriteriaJoin($"WHERE SifraPorudzbine={porudzbina.Id}", new StavkaPorudzbine()).OfType<StavkaPorudzbine>().ToList();
                _broker.Commit();
                return porudzbina;
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
