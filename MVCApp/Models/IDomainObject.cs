using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public interface IDomainObject
    {
        string TabelName { get; }
        string InsertValue { get; }
        string SearchCondition { get; }
        string ColumnNames { get; }
        string SetColumnValues { get; }
        string IdColumn { get; }
        int Id { get; }
        List<IDomainObject> VratiListu(SqlDataReader reader);
        IDomainObject VratiObjekat(SqlDataReader reader);
    }
}
