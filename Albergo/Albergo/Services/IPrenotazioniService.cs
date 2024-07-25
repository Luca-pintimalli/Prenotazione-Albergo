using System;
using Albergo.Models;

namespace Albergo.Services
{
	public interface IPrenotazioniService
	{
		void newPrenotazione(Prenotazione prenotazione);

		//INSERIMENTO PRENOTAZIONI
		IEnumerable<Prenotazione> GetPrenotazioni();

		//SINGOLA PRENOTAZIONE
		Prenotazione GetPrenotazione(int ID);

		//MODIFICA PRENOTAZIONE
		void UpdatePrenotazione(int ID, Prenotazione prenotazione);

		//Eliminazione
		void DeletePrenotazione(int ID);
	}
}

