namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeModificoTicketParaSMT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsSMTs", "turn", c => c.Int(nullable: false));
            AddColumn("dbo.Autorizantes", "dpto", c => c.String());
            AlterColumn("dbo.TicketsSMTs", "Creator", c => c.Int(nullable: false));
            AlterColumn("dbo.TicketsSMTs", "Authorizing", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketsSMTs", "Authorizing", c => c.String());
            AlterColumn("dbo.TicketsSMTs", "Creator", c => c.String());
            DropColumn("dbo.Autorizantes", "dpto");
            DropColumn("dbo.TicketsSMTs", "turn");
        }
    }
}
