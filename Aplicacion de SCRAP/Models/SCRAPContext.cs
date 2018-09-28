using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Aplicacion_de_SCRAP.Models
{
    public class SCRAPContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SCRAPContext() : base("name=SCRAPContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Tickets> Tickets { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.AreaScrap> AreaScraps { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.DefectCodes> DefectCodes { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.RootCause> RootCauses { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Line> Lines { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Naed> Naeds { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.PartNo> PartNoes { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Sub_Ensamble> Sub_Ensamble { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.TicketsSMT> TicketsSMTs { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.CauseSMT> CauseSMTs { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.CodesSMT> CodesSMTs { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Autorizantes> Autorizantes { get; set; }

        public System.Data.Entity.DbSet<Aplicacion_de_SCRAP.Models.Export> Ultimo { get; set; }

    }
}
