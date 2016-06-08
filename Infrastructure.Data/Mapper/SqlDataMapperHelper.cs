using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mapper
{
    internal static class SqlDataMapperHelper
    {
        public static string GetColumn(PropertyInfo property)
        {
            var columnAttribute = property.GetCustomAttribute<ColumnNameAttribute>();

            if (columnAttribute != null)
            {
                return ((ColumnNameAttribute)columnAttribute).Column;
            }

            return property.Name;
        }
    }
}
