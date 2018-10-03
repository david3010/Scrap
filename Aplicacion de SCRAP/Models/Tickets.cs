using Aplicacion_de_SCRAP.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion_de_SCRAP.Models
{
    public class Tickets
    {
        [Key]
        public int TicketScrapID { get; set; }

        [Display(Name = "Estado")]
        public TicketStatus TicketStatus { get; set; }

        //[Display(Name = "No. de empleado que creó el ticket")]
        //public int IdUsuario { get; set; }

        [Display(Name = "Usuario que creó el ticket")]
        public int Creator { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe de ser mayor a 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Linea")]
        public int LineID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Supervisor")]
        public int SupervisorID { get; set; }

        [NotMapped]
        [Display(Name = "Supervisor")]
        public string SupervisorName { get; set; }

        [NotMapped]
        [Display(Name = "Designacion de componente con defecto")]
        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [StringLength(3, ErrorMessage = "Solo puede escribir 3 caracteres como máximo", MinimumLength = 2)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "The characters ':', '.' ';', '*', '/' and '\' are not allowed")]
        public string Location { get; set; }

        [NotMapped]
        [Display(Name = "Designacion de componente con defecto")]
        [StringLength(3, ErrorMessage = "Solo puede escribir 3 caracteres como máximo", MinimumLength = 2)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "The characters ':', '.' ';', '*', '/' and '\' are not allowed")]
        public string Location1 { get; set; }

        [NotMapped]
        [Display(Name = "Designacion de componente con defecto")]
        [StringLength(3, ErrorMessage = "Solo puede escribir 3 caracteres como máximo", MinimumLength = 2)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "The characters ':', '.' ';', '*', '/' and '\' are not allowed")]
        public string Location2 { get; set; }

        [NotMapped]
        [Display(Name = "Designacion de componente con defecto")]
        [StringLength(3, ErrorMessage = "Solo puede escribir 3 caracteres como máximo", MinimumLength = 2)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "The characters ':', '.' ';', '*', '/' and '\' are not allowed")]
        public string Location3 { get; set; }

        [Display(Name = "Designacion de componente con defecto")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Area que generó el Scrap")]
        public int AreaSCRAPID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Naed")]
        public int NaedID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Sub-Ensamble")]
        public int Sub_EnsambleID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Número de parte")]
        public int PartNoID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Código de defecto")]
        public int DefectCodeID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Causa Raíz que Genero SCRAP")]
        public int RootCauseID { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Tipo de reparación")]
        public string Repair { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N4}")]
        public double Cost { get; set; }

        public int Turn { get; set; }
        public int? Authorizing { get; set; }

        public virtual AreaScrap AreaScrap { get; set; }
        public virtual DefectCodes DefectCodes { get; set; }
        public virtual RootCause RootCause { get; set; }
        public virtual Line Lines { get; set; }
        public virtual PartNo NoParts { get; set; }
        public virtual Naed Naeds { get; set; }
        public virtual Sub_Ensamble Sub_Ensambles { get; set; }
    }
}
