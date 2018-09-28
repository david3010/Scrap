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

        [Display(Name = "Causa")]
        public string Code { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public ICollection<TicketsSMT> Tickets { get; set; }
    }
}