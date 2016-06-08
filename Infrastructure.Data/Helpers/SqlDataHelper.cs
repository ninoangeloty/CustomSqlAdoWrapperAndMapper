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
        public static IEnumerable<TModel> ToModel<TModel>(string commandText, string connectionString, SqlDataQueryParameter[] parameters = null, bool isStoredProcedure = false)
            where TModel : class, new()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();

                    SetupCommand(cmd, parameters, isStoredProcedure);

                    return cmd.ExecuteReader().As<TModel>();
                }
            }
        }

        public static DataTable ToDataTable(string commandText, string connectionString, SqlDataQueryParameter[] parameters = null, bool isStoredProcedure = false)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();

                    SetupCommand(cmd, parameters, isStoredProcedure);

                    return cmd.ExecuteReader().ToDataTable();
                }
            }
        }

        private static void SetupCommand(SqlCommand cmd, SqlDataQueryParameter[] parameters, bool isStoredProcedure)
        {
            if (isStoredProcedure)
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            }

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Name, param.Value);
            }
        }
    }
}
