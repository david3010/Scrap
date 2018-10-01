using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class AreaScrap
    {
        [Key]
        public int AreaSCRAPID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Area que generó SCRAP")]
        public string NAreaScrap { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}