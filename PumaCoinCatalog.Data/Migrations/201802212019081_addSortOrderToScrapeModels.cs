namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSortOrderToScrapeModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrapeCoinCategory", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.ScrapeCoinCollection", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.ScrapeCoinType", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.ScrapeCoin", "SortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrapeCoin", "SortOrder");
            DropColumn("dbo.ScrapeCoinType", "SortOrder");
            DropColumn("dbo.ScrapeCoinCollection", "SortOrder");
            DropColumn("dbo.ScrapeCoinCategory", "SortOrder");
        }
    }
}
