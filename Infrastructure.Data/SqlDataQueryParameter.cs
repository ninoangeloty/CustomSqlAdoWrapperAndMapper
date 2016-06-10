using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SqlDataQueryParameter
    {
        public SqlDataQueryParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public SqlDataQueryParameter(string name, bool isOutput, SqlDbType sqlDbType)
        {
            this.Name = name;
            this.SqlDbType = sqlDbType;
            this.IsOutput = isOutput;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsOutput { get; set; }
        public SqlDbType SqlDbType { get; set; }
    }
}
