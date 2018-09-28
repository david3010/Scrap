using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class PartNoNaed
    {
        [Key]
        public int PartNoNaedID { get; set; }

        public int PartNoID { get; set; }

        public int NaedID { get; set; }

        public virtual PartNo PartNo { get; set; }
        public virtual Naed Naed { get; set; }

    }
}