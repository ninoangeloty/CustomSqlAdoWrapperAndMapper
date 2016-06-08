using Infrastructure.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SqlDataQuery
    {
        public SqlDataQuery(string commandText, string connectionString)
        {
            this.CommandText = commandText;
            this.ConnectionString = connectionString;
        }

        public SqlDataQuery AddParameter(string name, object value)
        {
            this.Parameters.Add(new SqlDataQueryParameter(name, value));

            return this;
        }

        public SqlDataQuery StoredProcedure()
        {
            this.IsStoredProcedure = true;

            return this;
        }

        public IEnumerable<TModel> As<TModel>()
            where TModel : class, new()
        {
            return SqlDataHelper.ToModel<TModel>(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), this.IsStoredProcedure);
        }

        public DataTable ToDataTable()
        {
            return SqlDataHelper.ToDataTable(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), this.IsStoredProcedure);
        }

        private bool IsStoredProcedure { get; set; }
        public string CommandText { get; private set; }
        public string ConnectionString { get; private set; }

        private List<SqlDataQueryParameter> _parameters;
        private List<SqlDataQueryParameter> Parameters
        {
            get
            {
                return _parameters ?? (_parameters = new List<SqlDataQueryParameter>());
            }

            set
            {
                _parameters = value;
            }
        }
    }
}
