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
        public List<IDomainObject> ReturnAll(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"SELECT * FROM {objekat.TabelName}";
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }

        public IDomainObject ReturnObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"SELECT * FROM {objekat.TabelName} {objekat.SearchCondition}";
            SqlDataReader reader = commmand.ExecuteReader();
            IDomainObject result = objekat.VratiObjekat(reader);
            reader.Close();
            return result;
        }

        public bool UpdateObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"UPDATE {objekat.TabelName} SET {objekat.SetColumnValues} {objekat.SearchCondition}";
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }
        public bool UpdateSpecific(IDomainObject objekat, string values)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"UPDATE {objekat.TabelName} SET {values} {objekat.SearchCondition}";
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public bool DeleteObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"DELETE FROM {objekat.TabelName} {objekat.SearchCondition}";
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public int InsertObject(IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"INSERT INTO {objekat.TabelName} {objekat.ColumnNames} OUTPUT inserted.Id VALUES ({objekat.InsertValue})";
            int id = (int)commmand.ExecuteScalar();
            return id;
        }

        public List<IDomainObject> ReturnByCriteria(string criteria, IDomainObject objekat)
        {
            SqlCommand commmand = new SqlCommand("", _connection, _transaction);
            commmand.CommandText = $"SELECT * FROM {objekat.TabelName} {criteria}";
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomainObject> result = objekat.VratiListu(reader);
            reader.Close();
            return result;
        }
    }
}
