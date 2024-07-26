using System;
namespace Albergo.Models
{
	public class Dipendenti
	{
		public int Id { get; set; }

		public required string NomeUtente { get; set; }

		public required string Password { get; set; }
	}
}

