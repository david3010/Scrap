using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models.ViewModels
{
    public class RolView
    {
        [Display(Name="Tipo de permiso")]
        public string RoleID { get; set; }

        public string Name { get; set; }
    }
}