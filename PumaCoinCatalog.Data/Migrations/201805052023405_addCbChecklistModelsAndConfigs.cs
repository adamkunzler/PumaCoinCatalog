namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCbChecklistModelsAndConfigs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CbChecklistCoin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InCollection = c.Boolean(nullable: false),
                        Grade = c.Short(nullable: false),
                        ValueEstimate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShouldExclude = c.Boolean(nullable: false),
                        Checklist_Id = c.Int(nullable: false),
                        Coin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbChecklist", t => t.Checklist_Id, cascadeDelete: false)
                .ForeignKey("dbo.CbCoin", t => t.Coin_Id, cascadeDelete: false)
                .Index(t => t.Checklist_Id)
                .Index(t => t.Coin_Id);
            
            CreateTable(
                "dbo.CbChecklist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        LastModified = c.DateTime(nullable: false),
                        Collection_Id = c.Int(nullable: false),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbCollection", t => t.Collection_Id, cascadeDelete: false)
                .ForeignKey("dbo.CbType", t => t.Type_Id, cascadeDelete: false)
                .Index(t => t.Collection_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.CbCollection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CbChecklistCoin", "Coin_Id", "dbo.CbCoin");
            DropForeignKey("dbo.CbChecklistCoin", "Checklist_Id", "dbo.CbChecklist");
            DropForeignKey("dbo.CbChecklist", "Type_Id", "dbo.CbType");
            DropForeignKey("dbo.CbChecklist", "Collection_Id", "dbo.CbCollection");
            DropIndex("dbo.CbChecklist", new[] { "Type_Id" });
            DropIndex("dbo.CbChecklist", new[] { "Collection_Id" });
            DropIndex("dbo.CbChecklistCoin", new[] { "Coin_Id" });
            DropIndex("dbo.CbChecklistCoin", new[] { "Checklist_Id" });
            DropTable("dbo.CbCollection");
            DropTable("dbo.CbChecklist");
            DropTable("dbo.CbChecklistCoin");
        }
    }
}
