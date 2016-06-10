using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Extensions;

namespace Infrastructure.Data.Helpers
{
    internal static class SqlDataHelper
    {
        public static IEnumerable<TModel> ToModel<TModel>(
            string commandText, string connectionString, SqlDataQueryParameter[] parameters = null, CommandType? commandType = null)
            where TModel : class, new()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();

                    SetupCommand(cmd, parameters, commandType);

                    return cmd.ExecuteReader().As<TModel>();
                }
            }
        }

        public static DataTable ToDataTable(
            string commandText, string connectionString, SqlDataQueryParameter[] parameters = null, CommandType? commandType = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();

                    SetupCommand(cmd, parameters, commandType);

                    return cmd.ExecuteReader().ToDataTable();
                }
            }
        }

        public static SqlDataQueryResult Execute(
            string commandText, string connectionString, SqlDataQueryParameter[] parameters = null, CommandType? commandType = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();

                    SetupCommand(cmd, parameters, commandType);

                    cmd.ExecuteNonQuery();
                                        
                    return new SqlDataQueryResult() { 
                        OutputParameters = GetOutputParameters(cmd, parameters)
                    };
                }
            }
        }

        private static IEnumerable<SqlDataQueryParameter> GetOutputParameters(SqlCommand command, SqlDataQueryParameter[] parameters)
        {
            foreach (var parameter in parameters.Where(_ => _.IsOutput))
            {
                yield return new SqlDataQueryParameter(parameter.Name, command.Parameters[parameter.Name].Value);
            }
        }

        private static void SetupCommand(SqlCommand cmd, SqlDataQueryParameter[] parameters, CommandType? commandType)
        {
            if (commandType.HasValue)
            {
                cmd.CommandType = commandType.Value;
            }

            foreach (var param in parameters)
            {
                if (param.IsOutput)
                {
                    cmd.Parameters.Add(param.Name, param.SqlDbType);
                    cmd.Parameters[param.Name].Direction = ParameterDirection.Output;
                }
                else
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
            }
        }
    }
}
