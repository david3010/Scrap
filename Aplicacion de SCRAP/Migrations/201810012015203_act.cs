namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class act : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DefectCodes", "DefectCode", c => c.String(nullable: false));
            AlterColumn("dbo.DefectCodes", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CauseSMTs", "Code", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.CauseSMTs", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CodesSMTs", "code", c => c.String(nullable: false));
            AlterColumn("dbo.CodesSMTs", "description", c => c.String(nullable: false));
            AlterColumn("dbo.PartNoes", "NPart", c => c.String(nullable: false));
            AlterColumn("dbo.PartNoes", "PartDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Sub_Ensamble", "Sub_Ensamble_Description", c => c.String(nullable: false));
            AlterColumn("dbo.RootCauses", "NRootCause", c => c.String(nullable: false));
            AlterColumn("dbo.RootCauses", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RootCauses", "Description", c => c.String());
            AlterColumn("dbo.RootCauses", "NRootCause", c => c.String());
            AlterColumn("dbo.Sub_Ensamble", "Sub_Ensamble_Description", c => c.String());
            AlterColumn("dbo.PartNoes", "PartDescription", c => c.String());
            AlterColumn("dbo.PartNoes", "NPart", c => c.String());
            AlterColumn("dbo.CodesSMTs", "description", c => c.String());
            AlterColumn("dbo.CodesSMTs", "code", c => c.String());
            AlterColumn("dbo.CauseSMTs", "Description", c => c.String());
            AlterColumn("dbo.CauseSMTs", "Code", c => c.String());
            AlterColumn("dbo.DefectCodes", "Description", c => c.String());
            AlterColumn("dbo.DefectCodes", "DefectCode", c => c.String());
        }
    }
}
