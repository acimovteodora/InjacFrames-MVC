using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.DatabaseBroker
{
    public class Broker
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public Broker(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            _connection.Open();
        }
        public void CloseConnection()
        {
            _connection.Close();
        }
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public List<IDomainObject> SelectAll(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT * FROM {objekat.TabelName}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }

        public IDomainObject SelectObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT * FROM {objekat.TabelName} {objekat.SearchCondition}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            IDomainObject result = objekat.VratiObjekat(reader);
            reader.Close();
            return result;
        }

        public bool UpdateObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"UPDATE {objekat.TabelName} SET {objekat.SetColumnValues} {objekat.SearchCondition}"
            };
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }
        public bool UpdateSpecific(IDomainObject objekat, string values)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"UPDATE {objekat.TabelName} SET {values} {objekat.SearchCondition}"
            };
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public bool DeleteObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"DELETE FROM {objekat.TabelName} {objekat.SearchCondition}"
            };
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public long InsertObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"INSERT INTO {objekat.TabelName} ({objekat.InsertColumns}) {objekat.Identifikator} {objekat.InsertValue}"
            };
            if(objekat.Identifikator == "")
            {
                if (commmand.ExecuteNonQuery() != 0) return 1;
                return 0;
            }
            long id = (long)commmand.ExecuteScalar();
            if (id != 0) return id;
            return 0;
        }
        public bool InsertObjectSpecific(IDomainObject objekat, string value)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"INSERT INTO {objekat.TabelName} {value}"
            };
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public List<IDomainObject> SelectAllJoin(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT {objekat.JoinSelect} {objekat.JoinTables}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }

        public IDomainObject SelectObjectJoin(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT {objekat.JoinSelect} {objekat.JoinTables} {objekat.SearchCondition}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            IDomainObject result = objekat.VratiObjekat(reader);
            reader.Close();
            return result;
        }

        public List<IDomainObject> ReturnByCriteria(string criteria, IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT * FROM {objekat.TabelName} {criteria}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }


        public List<IDomainObject> ReturnByCriteriaJoin(string criteria,IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"SELECT {objekat.JoinSelect} {objekat.JoinTables} {criteria}"
            };
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }
        public bool DeleteObjectCriteria(string criteria,IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction)
            {
                CommandText = $"DELETE FROM {objekat.TabelName} {criteria}"
            };
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }
    }
}
