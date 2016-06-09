using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string column)
        {
            this.Column = column;
        }

        public string Column { get; set; }
    }
}
