using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class CauseSMT
    {
        [Key]
        public int CauseSMTID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Origen (Grupo)")]
        [StringLength(1, ErrorMessage = "Solo puede escribir la letra del Grupo al que pertenece el código", MinimumLength = 1)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Los caracteres ':', '.' ';', '*', '/' y '\' no son admitidos")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public ICollection<TicketsSMT> Tickets { get; set; }
    }
}