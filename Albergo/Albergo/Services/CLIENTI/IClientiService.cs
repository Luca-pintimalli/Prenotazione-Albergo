using System;
using Albergo.Models;

namespace Albergo.Services.CLIENTI
{
	public interface IClientiService
	{
		void NewCliente(Cliente cliente);

		//INSERIMENTO TUTTI CLIENTI
		IEnumerable<Cliente> GetClienti();

		//recupero singolo cliente
		Cliente GetCliente(int ID);

        //MODIFICA DEL CLIENTE
        void UpdateCliente(int ID, Cliente cliente);

        //Elimina CLIENTE
        void DeleteCliente(int ID);
    }
}

