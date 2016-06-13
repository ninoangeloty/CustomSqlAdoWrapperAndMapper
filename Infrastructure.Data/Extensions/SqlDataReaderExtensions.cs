using Infrastructure.Data.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static IEnumerable<TModel> As<TModel>(this SqlDataReader reader)
            where TModel : class, new()
        {
            return SqlDataMapper.ToModel<TModel>(reader);
        }

        public static DataTable ToDataTable(this SqlDataReader reader)
        {
            var dataTable = new DataTable();
            dataTable.Load(reader);

            return dataTable;
        }
    }
}
