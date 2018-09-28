namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoLinea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "Tipo");
        }
    }
}
