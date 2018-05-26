namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retireVersion1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScrapeCoinCategory", "CoinCollection_Id", "dbo.ScrapeCoinCollection");
            DropForeignKey("dbo.ScrapeCoinType", "CoinCategory_Id", "dbo.ScrapeCoinCategory");
            DropForeignKey("dbo.ScrapeCoin", "CoinType_Id", "dbo.ScrapeCoinType");
            DropForeignKey("dbo.Checklist", "CoinCategory_Id", "dbo.ScrapeCoinCategory");
            DropForeignKey("dbo.Checklist", "CoinCollection_Id", "dbo.ScrapeCoinCollection");
            DropForeignKey("dbo.Checklist", "CoinType_Id", "dbo.ScrapeCoinType");
            DropForeignKey("dbo.ChecklistCoin", "Checklist_Id", "dbo.Checklist");
            DropForeignKey("dbo.ChecklistCoin", "Coin_Id", "dbo.ScrapeCoin");
            DropIndex("dbo.ChecklistCoin", new[] { "Checklist_Id" });
            DropIndex("dbo.ChecklistCoin", new[] { "Coin_Id" });
            DropIndex("dbo.Checklist", new[] { "CoinCategory_Id" });
            DropIndex("dbo.Checklist", new[] { "CoinCollection_Id" });
            DropIndex("dbo.Checklist", new[] { "CoinType_Id" });
            DropIndex("dbo.ScrapeCoinCategory", new[] { "CoinCollection_Id" });
            DropIndex("dbo.ScrapeCoinType", new[] { "CoinCategory_Id" });
            DropIndex("dbo.ScrapeCoin", new[] { "CoinType_Id" });
            DropTable("dbo.ChecklistCoin");
            DropTable("dbo.Checklist");
            DropTable("dbo.ScrapeCoinCategory");
            DropTable("dbo.ScrapeCoinCollection");
            DropTable("dbo.ScrapeCoinType");
            DropTable("dbo.ScrapeCoin");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScrapeCoin",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Year = c.String(nullable: false, maxLength: 8),
                        Variety = c.String(maxLength: 64),
                        Mintage = c.Int(nullable: false),
                        KmNumber = c.String(maxLength: 16),
                        NumisMediaId = c.Int(nullable: false),
                        NgcId = c.Int(nullable: false),
                        PcgsId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        CoinType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScrapeCoinType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 64),
                        Details = c.String(maxLength: 64),
                        Base64Image = c.String(),
                        SortOrder = c.Int(nullable: false),
                        BullionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoinCategory_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScrapeCoinCollection",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 64),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScrapeCoinCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 64),
                        SortOrder = c.Int(nullable: false),
                        Base64Image = c.String(),
                        FaceValue = c.Short(nullable: false),
                        CoinCollection_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checklist",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        LastModified = c.DateTime(nullable: false),
                        CoinCategory_Id = c.Guid(nullable: false),
                        CoinCollection_Id = c.Guid(nullable: false),
                        CoinType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChecklistCoin",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        InCollection = c.Boolean(nullable: false),
                        AdamGrade = c.Short(),
                        ValueEstimate = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ShouldExclude = c.Boolean(nullable: false),
                        Checklist_Id = c.Guid(nullable: false),
                        Coin_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ScrapeCoin", "CoinType_Id");
            CreateIndex("dbo.ScrapeCoinType", "CoinCategory_Id");
            CreateIndex("dbo.ScrapeCoinCategory", "CoinCollection_Id");
            CreateIndex("dbo.Checklist", "CoinType_Id");
            CreateIndex("dbo.Checklist", "CoinCollection_Id");
            CreateIndex("dbo.Checklist", "CoinCategory_Id");
            CreateIndex("dbo.ChecklistCoin", "Coin_Id");
            CreateIndex("dbo.ChecklistCoin", "Checklist_Id");
            AddForeignKey("dbo.ChecklistCoin", "Coin_Id", "dbo.ScrapeCoin", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChecklistCoin", "Checklist_Id", "dbo.Checklist", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Checklist", "CoinType_Id", "dbo.ScrapeCoinType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Checklist", "CoinCollection_Id", "dbo.ScrapeCoinCollection", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Checklist", "CoinCategory_Id", "dbo.ScrapeCoinCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScrapeCoin", "CoinType_Id", "dbo.ScrapeCoinType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScrapeCoinType", "CoinCategory_Id", "dbo.ScrapeCoinCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScrapeCoinCategory", "CoinCollection_Id", "dbo.ScrapeCoinCollection", "Id", cascadeDelete: true);
        }
    }
}
