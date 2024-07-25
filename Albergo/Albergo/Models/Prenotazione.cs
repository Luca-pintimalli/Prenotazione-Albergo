using System;
namespace Albergo.Models
{
	public class Prenotazione
	{
        public int ID { get; set; }
        public string CodiceFiscale { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivo { get; set; }
        public int Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string Dettagli { get; set; }
    }
}

