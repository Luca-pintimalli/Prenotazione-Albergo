using System;
using Albergo.Models;

namespace Albergo.Models
{
	public class PrenotazioneForm
	{
        public Prenotazione Prenotazione { get; set; }
        public IEnumerable<Camera> Camere { get; set; }
        public IEnumerable<Cliente> Clienti { get; set; }
    }
}
