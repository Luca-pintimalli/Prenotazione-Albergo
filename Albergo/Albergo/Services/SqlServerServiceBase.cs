using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Albergo.Services
{
	public class SqlServerServiceBase : ServiceBase
    {

        private SqlConnection _connection;

		public SqlServerServiceBase(IConfiguration config )
		{
            _connection = new SqlConnection(config.GetConnectionString("AppDb"));
		}

       

        protected override DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

        protected override DbConnection GetConnection()
        {
            return _connection;
        }
    }
}

