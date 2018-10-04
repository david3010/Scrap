namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOptionCodeSMT : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketsSMTs", "CodesSMTId", "dbo.CodesSMTs");
            DropIndex("dbo.TicketsSMTs", new[] { "CodesSMTId" });
            DropColumn("dbo.TicketsSMTs", "CodesSMTId");
            DropTable("dbo.CodesSMTs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CodesSMTs",
                c => new
                    {
                        CodesSMTId = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CodesSMTId);
            
            AddColumn("dbo.TicketsSMTs", "CodesSMTId", c => c.Int(nullable: false));
            CreateIndex("dbo.TicketsSMTs", "CodesSMTId");
            AddForeignKey("dbo.TicketsSMTs", "CodesSMTId", "dbo.CodesSMTs", "CodesSMTId");
        }
    }
}
