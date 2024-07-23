using System;
using System.Data.Common;

namespace Albergo.Services
{
	public abstract class ServiceBase
	{
		//METODO CHE RESTITUISCE LA CONNESIONE AL SERVER 
		protected abstract DbConnection GetConnection();

		//METODO CHE CREA UN COMANDO
		protected abstract DbCommand GetCommand(string commandText);
	}
}

