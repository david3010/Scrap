using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Sub_EnsambleNaeds
    {
        [Key]
        public int Sub_EnsambleNaedsID { get; set; }
        public int Sub_EnsambleID { get; set; }
        public int NaedID { get; set; }

        public virtual Sub_Ensamble Sub_Ensamble { get; set; }
        public virtual Naed Naed { get; set; }
    }
}