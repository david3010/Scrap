using Aplicacion_de_SCRAP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models.ViewModels
{
    public class UserView
    {
        [Display(Name="No. de empleado")]
        public string UserID { get; set; }

        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name="Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public RolView Role { get; set; }

        public List<RolView> Roles { get; set; }
    }
}