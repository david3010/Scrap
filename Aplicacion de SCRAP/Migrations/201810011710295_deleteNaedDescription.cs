namespace Aplicacion_de_SCRAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteNaedDescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Naeds", "NaedDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Naeds", "NaedDescription", c => c.String());
        }
    }
}
