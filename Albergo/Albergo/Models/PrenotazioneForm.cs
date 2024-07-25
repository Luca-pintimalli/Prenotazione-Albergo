using System;
using Albergo.Models;

namespace Albergo.Models
{
    public class PrenotazioneForm
    {
        public Prenotazione Prenotazione { get; set; }
        public List<Cliente> Clienti { get; set; } = new List<Cliente>();
        public List<Camera> Camere { get; set; } = new List<Camera>();
    }

}