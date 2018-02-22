namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeImageUris : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ScrapeCoinCategory", "ImageUrl");
            DropColumn("dbo.ScrapeCoinType", "ImageUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScrapeCoinType", "ImageUri", c => c.String(maxLength: 1024));
            AddColumn("dbo.ScrapeCoinCategory", "ImageUrl", c => c.String(maxLength: 1024));
        }
    }
}
