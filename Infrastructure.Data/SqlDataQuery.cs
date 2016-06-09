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

        public SqlDataQuery AddOutputParameter(string name, SqlDbType sqlDbType)
        {
            this.Parameters.Add(new SqlDataQueryParameter(name, sqlDbType));

            return this;
        }

        public SqlDataQuery IsStoredProcedure()
        {
            _isStoredProcedure = true;

            return this;
        }

        public IEnumerable<TModel> Fetch<TModel>()
            where TModel : class, new()
        {
            return SqlDataHelper.ToModel<TModel>(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), _isStoredProcedure);
        }

        public DataTable FetchAsDataTable()
        {
            return SqlDataHelper.ToDataTable(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), _isStoredProcedure);
        }

        public void Execute()
        {
            SqlDataHelper.Execute(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), _isStoredProcedure);
        }

        public string CommandText { get; private set; }
        public string ConnectionString { get; private set; }
        
        private bool _isStoredProcedure { get; set; }
        private List<SqlDataQueryParameter> _parameters;
        public List<SqlDataQueryParameter> Parameters
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
