using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public interface IDomainObject
    {
        string Identifikator { get; }
        string TabelName { get; }
        string InsertColumns { get; }
        string InsertValue { get; }
        string ColumnNames { get; }
        string SetColumnValues { get; }
        string SearchCondition { get; }
        List<IDomainObject> VratiListu(SqlDataReader reader);
        IDomainObject VratiObjekat(SqlDataReader reader);
        string JoinSelect { get; }
        string JoinTables { get; }
    }
}
