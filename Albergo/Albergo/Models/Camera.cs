using System;
using System.ComponentModel.DataAnnotations;

namespace Albergo.Models
{
	public class Camera
	{
        [Key]
        public int Numero { get; set; }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Descrizione { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }
    }
}

