using Infrastructure.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            this.CommandType = System.Data.CommandType.StoredProcedure;

            return this;
        }

        public IEnumerable<TModel> Fetch<TModel>()
            where TModel : class, new()
        {
            return SqlDataHelper.ToModel<TModel>(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), this.CommandType);
        }

        public DataTable FetchAsDataTable()
        {
            return SqlDataHelper.ToDataTable(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), this.CommandType);
        }

        public SqlDataQueryResult Execute()
        {
            return SqlDataHelper.Execute(this.CommandText, this.ConnectionString, this.Parameters.ToArray(), this.CommandType);
        }

        public string CommandText { get; set; }
        public string ConnectionString { get; private set; }
        public CommandType CommandType { get; set; }
        
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
