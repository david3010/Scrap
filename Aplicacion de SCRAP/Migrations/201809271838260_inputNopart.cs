namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inputNopart : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TicketsSMTs", new[] { "PartNoID" });
            RenameColumn(table: "dbo.TicketsSMTs", name: "PartNoID", newName: "NoParts_PartNoID");
            AddColumn("dbo.TicketsSMTs", "PartNo", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.TicketsSMTs", "NoParts_PartNoID", c => c.Int());
            CreateIndex("dbo.TicketsSMTs", "NoParts_PartNoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TicketsSMTs", new[] { "NoParts_PartNoID" });
            AlterColumn("dbo.TicketsSMTs", "NoParts_PartNoID", c => c.Int(nullable: false));
            DropColumn("dbo.TicketsSMTs", "PartNo");
            RenameColumn(table: "dbo.TicketsSMTs", name: "NoParts_PartNoID", newName: "PartNoID");
            CreateIndex("dbo.TicketsSMTs", "PartNoID");
        }
    }
}
