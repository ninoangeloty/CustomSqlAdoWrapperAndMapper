using Infrastructure.Data.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Extensions
{
    public static class DataTableExtensions
    {
        public static IEnumerable<TModel> As<TModel>(this DataTable table) where TModel : new()
        {
            if (table.Rows.Count == 0)
            {
                return new List<TModel>();
            }

            var modelProperties = Activator.CreateInstance<TModel>().GetType().GetProperties();
            var columns = GetDataTableColumns(table);

            return table.Rows.Cast<DataRow>().Select(row =>
            {
                var obj = new TModel();

                foreach (var prop in modelProperties)
                {
                    var column = SqlDataMapperHelper.GetColumn(prop);

                    if (columns.Contains(column) && row[column] != DBNull.Value)
                    {
                        prop.SetValue(obj, row[column]);
                    }
                }

                return obj;
            });
        }

        private static IEnumerable<string> GetDataTableColumns(DataTable table)
        {
            return table.Columns.Cast<DataColumn>().Select(_ => _.ColumnName);
        }
    }
}
