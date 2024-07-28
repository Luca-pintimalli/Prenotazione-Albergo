using System;
using Albergo.Models;

namespace Albergo.Services.SERVIZI
{
	public interface IServiziPrenotazioneService
	{
        //LISTA COMPLETA Servizi Prenotazione
        IEnumerable<ServizioPrenotazione> GetServiziPrenotazione();

        //SINGOLO SERVIZIO 
        ServizioPrenotazione GetServizioPrenotazione(int id);

        //NUOVO SERVIZIO AGGIUNTO 
        void NewServizioPrenotazione(ServizioPrenotazione servizioPrenotazione);

        //MODIFICA SERVIZIO
        void UpdateServizioPrenotazione(int id, ServizioPrenotazione servizioPrenotazione);

        //ELIMINAZIONE SERVIZIO
        void DeleteServizioPrenotazione(int id);
    }
}

