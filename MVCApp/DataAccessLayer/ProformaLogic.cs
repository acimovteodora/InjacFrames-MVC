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
    public class ProformaLogic : IProformaLogic
    {
        private readonly Broker _broker;
        public ProformaLogic(Broker broker)
        {
            _broker = broker;
        }

        public Lajsna GetLajsna(string name)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Lajsna lajsna = _broker.ReturnByCriteria($"WHERE NazivLajsne LIKE '{name}'", new Lajsna()).OfType<Lajsna>().ToList().FirstOrDefault();
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

        public List<Lajsna> GetLajsne(List<Lajsna> lajsne)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                lajsne.AddRange(_broker.SelectAll(new Lajsna()).OfType<Lajsna>().ToList());
                lajsne = lajsne.Distinct().OfType<Lajsna>().ToList();
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

        public List<Banka> GetBanke()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Banka> list = _broker.SelectAll(new Banka()).OfType<Banka>().ToList();
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

        public List<Racun> GetRacuni()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Racun> list = _broker.SelectAllJoin(new Racun()).OfType<Racun>().ToList();
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

        public List<Proforma> SelectAll(Proforma objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                List<Proforma> list = _broker.SelectAllJoin(objekat).OfType<Proforma>().ToList();
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

        public Proforma SelectObject(Proforma objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Proforma proforma = _broker.SelectObjectJoin(objekat) as Proforma;
                proforma.Stavke = _broker.ReturnByCriteriaJoin($"WHERE sp.SifraProforme={proforma.Id}", new StavkaProforme()).OfType<StavkaProforme>().ToList();
                _broker.Commit();
                return proforma;
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

        public bool UpdateObject(Proforma objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Proforma fromDb = _broker.SelectObjectJoin(objekat) as Proforma;
                if(fromDb.UkupnaCena != objekat.UkupnaCena)
                {
                    if (!_broker.UpdateSpecific(objekat, $" UkupnaCena={objekat.UkupnaCena}")) throw new Exception();
                }
                else if(objekat.Popust == null)
                {
                    if (!_broker.UpdateSpecific(objekat, $"Datum='{objekat.Datum}',SifraZaposlenog={objekat.Zaposleni.Id},SifraPorudzbine={objekat.Porudzbina.Id},SifraBanke={objekat.Racun.Banka.Id},SifraRacuna={objekat.Racun.Id},SifraIsporuke={objekat.NacinIsporuke.Id}")) throw new Exception();
                }
                else if (!_broker.UpdateObject(objekat)) throw new Exception();
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

        public bool CreateObject(Racun objekat)
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                objekat.Banka = _broker.ReturnByCriteria($"WHERE Naziv LIKE '{objekat.Banka.Naziv}'", objekat.Banka).OfType<Banka>().ToList().FirstOrDefault();
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
    }
}
