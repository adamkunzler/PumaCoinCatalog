namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedChecklistAndChecklistCoin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChecklistCoin",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        InCollection = c.Boolean(nullable: false),
                        AdamGrade = c.Short(),
                        ValueEstimate = c.Decimal(precision: 18, scale: 2),
                        Checklist_Id = c.Guid(nullable: false),
                        Coin_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklist", t => t.Checklist_Id, cascadeDelete: false)
                .ForeignKey("dbo.ScrapeCoin", t => t.Coin_Id, cascadeDelete: false)
                .Index(t => t.Checklist_Id)
                .Index(t => t.Coin_Id);
            
            CreateTable(
                "dbo.Checklist",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false, maxLength: 128),
                        LastModified = c.DateTime(nullable: false, defaultValue: DateTime.UtcNow),
                        CoinCategory_Id = c.Guid(nullable: false),
                        CoinCollection_Id = c.Guid(nullable: false),
                        CoinType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrapeCoinCategory", t => t.CoinCategory_Id, cascadeDelete: false)
                .ForeignKey("dbo.ScrapeCoinCollection", t => t.CoinCollection_Id, cascadeDelete: false)
                .ForeignKey("dbo.ScrapeCoinType", t => t.CoinType_Id, cascadeDelete: false)
                .Index(t => t.CoinCategory_Id)
                .Index(t => t.CoinCollection_Id)
                .Index(t => t.CoinType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChecklistCoin", "Coin_Id", "dbo.ScrapeCoin");
            DropForeignKey("dbo.ChecklistCoin", "Checklist_Id", "dbo.Checklist");
            DropForeignKey("dbo.Checklist", "CoinType_Id", "dbo.ScrapeCoinType");
            DropForeignKey("dbo.Checklist", "CoinCollection_Id", "dbo.ScrapeCoinCollection");
            DropForeignKey("dbo.Checklist", "CoinCategory_Id", "dbo.ScrapeCoinCategory");
            DropIndex("dbo.Checklist", new[] { "CoinType_Id" });
            DropIndex("dbo.Checklist", new[] { "CoinCollection_Id" });
            DropIndex("dbo.Checklist", new[] { "CoinCategory_Id" });
            DropIndex("dbo.ChecklistCoin", new[] { "Coin_Id" });
            DropIndex("dbo.ChecklistCoin", new[] { "Checklist_Id" });
            DropTable("dbo.Checklist");
            DropTable("dbo.ChecklistCoin");
        }
    }
}
