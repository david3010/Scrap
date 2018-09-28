namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLineTipo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lines", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lines", "Tipo", c => c.String(nullable: false));
        }
    }
}
