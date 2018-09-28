using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Line
    {
        [Key]
        public int LineID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "ID de linea en CTS")]
        public int IIdLinea { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Linea")]
        public string LineName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo Back-End/Front-End")]
        public int Tipo { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<TicketsSMT> TicketsSMT { get; set; }
    }

    public enum Tipo
    {
        BackEnd,
        FrontEnd
    }
}