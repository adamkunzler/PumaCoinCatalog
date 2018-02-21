namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addScrapeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScrapeCoinCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false, maxLength: 64),
                        ImageUrl = c.String(maxLength: 1024),
                        CoinCollection_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrapeCoinCollection", t => t.CoinCollection_Id, cascadeDelete: false)
                .Index(t => t.CoinCollection_Id);
            
            CreateTable(
                "dbo.ScrapeCoinCollection",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScrapeCoinType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false, maxLength: 64),
                        Details = c.String(maxLength: 64),
                        ImageUri = c.String(maxLength: 1024),
                        CoinCategory_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrapeCoinCategory", t => t.CoinCategory_Id, cascadeDelete: false)
                .Index(t => t.CoinCategory_Id);
            
            CreateTable(
                "dbo.ScrapeCoin",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Year = c.String(nullable: false, maxLength: 8),
                        Variety = c.String(maxLength: 64),
                        Mintage = c.Int(nullable: false),
                        KmNumber = c.String(maxLength: 16),
                        NumisMediaId = c.Int(nullable: false),
                        NgcId = c.Int(nullable: false),
                        PcgsId = c.Int(nullable: false),
                        CoinType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrapeCoinType", t => t.CoinType_Id, cascadeDelete: false)
                .Index(t => t.CoinType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrapeCoin", "CoinType_Id", "dbo.ScrapeCoinType");
            DropForeignKey("dbo.ScrapeCoinType", "CoinCategory_Id", "dbo.ScrapeCoinCategory");
            DropForeignKey("dbo.ScrapeCoinCategory", "CoinCollection_Id", "dbo.ScrapeCoinCollection");
            DropIndex("dbo.ScrapeCoin", new[] { "CoinType_Id" });
            DropIndex("dbo.ScrapeCoinType", new[] { "CoinCategory_Id" });
            DropIndex("dbo.ScrapeCoinCategory", new[] { "CoinCollection_Id" });
            DropTable("dbo.ScrapeCoin");
            DropTable("dbo.ScrapeCoinType");
            DropTable("dbo.ScrapeCoinCollection");
            DropTable("dbo.ScrapeCoinCategory");
        }
    }
}
