using System;
using System.ComponentModel.DataAnnotations;

namespace Albergo.Models
{
	public class Cliente
	{
        [Key]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Citta { get; set; }

        [StringLength(50)]
        public string Provincia { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(15)]
        public string Cellulare { get; set; }
    }
}


