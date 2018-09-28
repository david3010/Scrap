using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class TicketsSMT
    {
        [Key]
        public int TicketSMTID { get; set; }

        [Display(Name = "Estado")]
        public TicketStatus TicketStatus { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Sub-Ensamble")]
        public int Sub_EnsambleID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Linea")]
        public int LineID { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Código Asignado")]
        public int CodesSMTId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo {0}")]
        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe de ser mayor a 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Número de parte")]
        [StringLength(12, ErrorMessage = "Solo puede escribir 12 caracteres como máximo", MinimumLength = 5)]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "The characters ':', '.' ';', '*', '/' and '\' are not allowed")]
        public string PartNo { get; set; }

        [Required(ErrorMessage = "Usted debe de llenar el campo de {0}")]
        [Display(Name = "Cáusa")]
        public int CauseSMTID { get; set; }

        [Display(Name = "Usuario que creó el ticket")]
        public int Creator { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Display(Name = "Costo")]
        public float Cost { get; set; }

        [Display(Name = "Autorizó")]
        public int Authorizing { get; set; }

        public int turn { get; set; }

        public virtual CodesSMT CodesSMT { get; set; }
        public virtual CauseSMT CauseSMT { get; set; }
        public virtual Line Lines { get; set; }
        public virtual PartNo NoParts { get; set; }
        public virtual Sub_Ensamble Sub_Ensambles { get; set; }
    }
}