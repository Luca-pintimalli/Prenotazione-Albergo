using System;
namespace Albergo.Models
{
    public class ServizioPrenotazioneForm
    {

        public ServizioPrenotazione ServizioPrenotazione { get; set; }
        public List<PrenotazioneDetails> Prenotazioni { get; set; } = new List<PrenotazioneDetails>();
        public List<Servizio> Servizi { get; set; } = new List<Servizio>();

    }
}

