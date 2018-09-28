using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class CodesSMT
    {
        [Key]
        public int CodesSMTId { get; set; }

        [Display(Name = "Código Asignado")]
        public string code { get; set; }

        [Display(Name = "Descripción")]
        public string description { get; set; }
    }
}