using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string column)
        {
            this.Column = column;
        }

        public string Column { get; set; }
    }
}
