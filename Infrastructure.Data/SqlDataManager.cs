﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Infrastructure.Data
{
    public class SqlDataManager
    {
        public SqlDataManager(string key)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public SqlDataQuery CommandText(string commandText)
        {
            return new SqlDataQuery(commandText, this.ConnectionString);
        }

        public string ConnectionString { get; private set; }
    }
}
