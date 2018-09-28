namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLineIdCTS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "IIdLinea", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "IIdLinea");
        }
    }
}
