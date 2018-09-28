using Aplicacion_de_SCRAP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class Autorizantes
    {
        [Key]
        public int AutorizantesID { get; set; }

        public int TicketID { get; set; }

        public int UserId { get; set; }

        public string Position { get; set; }

        public string UserName { get; set; }

        public Nullable<System.DateTime> FechaDeAutorizacion { get; set; }

        public string dpto { get; set; }

        public bool Checked { get; set; }

    }
}