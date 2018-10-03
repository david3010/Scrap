namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currency1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "Cost", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "Cost", c => c.Double(nullable: false));
        }
    }
}
