using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Naed
    {
        [Key]
        public int NaedID { get; set; }
        [Display(Name = "NAED")]
        public int NNAED { get; set; }
        public string NaedDescription { get; set; }

        public virtual ICollection<Sub_EnsambleNaeds> Sub_EnsambleNaeds { get; set; }
        public virtual ICollection<PartNoNaed> PartNoNaeds { get; set; }
    }
}