using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Export
    {
        [Key]
        public int idUltimo { get; set; }

        public int ultimo { get; set; }

        public int bf { get; set; }
    }
}