namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExportarExcel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exports",
                c => new
                    {
                        idUltimo = c.Int(nullable: false, identity: true),
                        ultimo = c.Int(nullable: false),
                        bf = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idUltimo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Exports");
        }
    }
}
