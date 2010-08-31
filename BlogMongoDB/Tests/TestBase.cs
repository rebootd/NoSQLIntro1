using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BlogMongoDB.Tests
{
    public class TestBase
    {
        private static readonly string _connectionStringHost = ConfigurationManager.AppSettings["connectionStringHost"];

        public string ConnectionString()
        {
            return ConnectionString(null, null, null, null);
        }

        public string ConnectionString(string query, string database, string userName, string password)
        {
            var authentication = string.Empty;
            if (userName != null)
            {
                authentication = string.Concat(userName, ':', password, '@');
            }
            if (!string.IsNullOrEmpty(query) && !query.StartsWith("?"))
            {
                query = string.Concat('?', query);
            }
            var host = string.IsNullOrEmpty(_connectionStringHost) ? "localhost" : _connectionStringHost;
            database = database ?? "NormTests";
            return string.Format("mongodb://{0}{1}/{2}{3}", authentication, host, database, query);
        }
    }
}