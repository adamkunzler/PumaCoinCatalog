namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBase64ImageProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrapeCoinCategory", "Base64Image", c => c.String());
            AddColumn("dbo.ScrapeCoinType", "Base64Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrapeCoinType", "Base64Image");
            DropColumn("dbo.ScrapeCoinCategory", "Base64Image");
        }
    }
}
