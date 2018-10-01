using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Naed
    {
        [Key]
        public int NaedID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "NAED")]
        public int NNAED { get; set; }

        [NotMapped]
        [Display(Name = "Descripción")]
        public string NaedDescription { get; set; }

        public virtual ICollection<Sub_EnsambleNaeds> Sub_EnsambleNaeds { get; set; }
        public virtual ICollection<PartNoNaed> PartNoNaeds { get; set; }
    }
}