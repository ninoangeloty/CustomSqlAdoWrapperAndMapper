using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Infrastructure.Data
{
    public class SqlDataManager : IDisposable
    {
        public SqlDataManager(string key)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public SqlDataQuery CreateCommand(string commandText)
        {
            return new SqlDataQuery(commandText, this.ConnectionString);
        }

        public void Dispose()
        {
            this.ConnectionString = string.Empty;
        }

        public string ConnectionString { get; private set; }
    }
}
