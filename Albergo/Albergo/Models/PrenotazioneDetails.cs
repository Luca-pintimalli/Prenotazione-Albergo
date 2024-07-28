using System;
using System.Collections.Generic;

namespace Albergo.Models
{
    public class PrenotazioneDetails
    {
        public int ID { get; set; }
        public string CodiceFiscale { get; set; }
        public string NomeCliente { get; set; }
        public string CognomeCliente { get; set; }
        public int NumeroCamera { get; set; }
        public string DescrizioneCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivo { get; set; }
        public int Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string Dettagli { get; set; }
        public int TotalPrenotazioni { get; set; }
        public int MezzaPensioneCount { get; set; }
        public int PensioneCompletaCount { get; set; }
        public int PernottamentoColazioneCount { get; set; }
        public IEnumerable<PrenotazioneDetails> Prenotazioni { get; set; }
    }
}
