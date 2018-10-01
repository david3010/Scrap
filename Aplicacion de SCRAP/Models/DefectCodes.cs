using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class DefectCodes
    {
        [Key]
        public int DefectCodeID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Código de Defecto de SCRAP")]
        public string DefectCode { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}