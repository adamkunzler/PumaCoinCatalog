namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFaceValueToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrapeCoinCategory", "FaceValue", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrapeCoinCategory", "FaceValue");
        }
    }
}
