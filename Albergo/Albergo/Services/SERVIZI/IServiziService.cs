using System;
using Albergo.Models;

namespace Albergo.Services.Servizi
{
	public interface IServiziService
	{
        void NewServizio(Servizio servizio);

        //INSERIMENTO TUTTI Servizi
        IEnumerable<Servizio> GetServizi();

        //recupero singolo servizio
        Servizio GetServizio(int id);

        //MODIFICA DEL Servizio
        void UpdateServizio(int id, Servizio servizio);

        //Elimina Servizio
        void DeleteServizio(int id);
    }
}

