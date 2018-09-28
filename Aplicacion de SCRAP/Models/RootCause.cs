using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class RootCause
    {
        [Key]
        public int RootCauseID { get; set; }

        [Display(Name = "Causa Raíz que generó el SCRAP")]
        public string NRootCause { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public ICollection<Tickets> Tickets { get; set; }
    }
}