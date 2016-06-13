using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mapper
{
    public static class SqlDataMapper
    {
        public static IEnumerable<TModel> ToModel<TModel>(SqlDataReader reader)
            where TModel : class, new()
        {
            var columns = SqlDataMapper.GetColumnNames(reader);
            var collection = new List<TModel>();
            var properties = typeof(TModel).GetProperties();

            while (reader.Read())
            {
                var model = new TModel();

                foreach (var property in properties)
                {
                    var column = SqlDataMapperHelper.GetColumn(property);

                    if (columns.Contains(column))
                    {
                        property.SetValue(model, reader[column]);
                    }
                }

                collection.Add(model);
            }

            return collection;
        }

        private static IEnumerable<string> GetColumnNames(SqlDataReader reader)
        {
            var columns = new List<string>();
            var count = reader.VisibleFieldCount;

            for (int i = 0; i < count; i++)
            {
                columns.Add(reader.GetName(i));
            }

            return columns;
        }
    }
}
