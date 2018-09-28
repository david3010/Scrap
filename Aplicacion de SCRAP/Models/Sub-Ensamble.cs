using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Sub_Ensamble
    {
        [Key]
        public int Sub_EnsambleID { get; set; }

        [Display(Name = "Sub-Ensamble")]
        public string Sub_Ensamble_Description { get; set; }

        public virtual ICollection<Sub_EnsambleNaeds> Sub_EnsambleNaeds { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<TicketsSMT> TicketsSMT { get; set; }
    }
}