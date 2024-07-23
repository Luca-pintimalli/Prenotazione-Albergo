using System;
using System.ComponentModel.DataAnnotations;

namespace Albergo.Models
{
	public class Camere
	{
        [Key]
        public int Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string Descrizione { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }
    }
}

