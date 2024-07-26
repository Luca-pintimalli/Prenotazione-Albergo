using System;
using System.ComponentModel.DataAnnotations;

namespace Albergo.Models
{
	public class Camera
	{
        [Key]
        [Display(Name ="Numero Camera")]
        [Required(ErrorMessage = "Inserire il numero della camera")]

        public int Numero { get; set; }

        public int ID { get; set; }

        [Required(ErrorMessage ="Descrivere la camera")]
        [StringLength(100)]
        [Display(Name = "Descrizione Della Camera")]

        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Inserire la tipologia")]

        [StringLength(50)]
        [Display(Name = "Tipologia")]

        public string Tipologia { get; set; }
    }
}

