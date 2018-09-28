namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Turno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Turn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Turn");
        }
    }
}
