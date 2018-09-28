using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class PartNo
    {
        [Key]
        public int PartNoID { get; set; }

        [Display(Name = "Número de parte")]
        public string NPart { get; set; }

        [Display(Name = "Descripción")]
        public string PartDescription { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Display(Name = "Costo")]
        public float UnitPrice { get; set; }

        public virtual ICollection<PartNoNaed> PartNoNaeds { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<TicketsSMT> TicketsSMT { get; set; }
    }
}