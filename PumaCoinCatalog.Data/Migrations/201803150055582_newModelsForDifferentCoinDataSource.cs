namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModelsForDifferentCoinDataSource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CbCoin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        MintMark = c.String(nullable: false, maxLength: 8),
                        Details = c.String(nullable: false, maxLength: 64),
                        Mintage = c.Long(nullable: false),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbType", t => t.Type_Id, cascadeDelete: false)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.CbType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        BeginDate = c.Short(nullable: false),
                        EndDate = c.Short(nullable: false),
                        MetalComposition = c.String(nullable: false, maxLength: 128),
                        Diameter = c.Single(nullable: false),
                        Mass = c.Single(nullable: false),
                        MeltValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUri = c.String(),
                        Variety_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbVariety", t => t.Variety_Id, cascadeDelete: false)
                .Index(t => t.Variety_Id);
            
            CreateTable(
                "dbo.CbVariety",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        SourceUri = c.String(nullable: false, maxLength: 256),
                        ImageUri = c.String(),
                        Denomination_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbDenomination", t => t.Denomination_Id, cascadeDelete: false)
                .Index(t => t.Denomination_Id);
            
            CreateTable(
                "dbo.CbDenomination",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        FaceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SourceUri = c.String(nullable: false, maxLength: 256),
                        ImageUri = c.String(),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbCountry", t => t.Country_Id, cascadeDelete: false)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.CbCountry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        ImageUri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CbCoin", "Type_Id", "dbo.CbType");
            DropForeignKey("dbo.CbType", "Variety_Id", "dbo.CbVariety");
            DropForeignKey("dbo.CbVariety", "Denomination_Id", "dbo.CbDenomination");
            DropForeignKey("dbo.CbDenomination", "Country_Id", "dbo.CbCountry");
            DropIndex("dbo.CbDenomination", new[] { "Country_Id" });
            DropIndex("dbo.CbVariety", new[] { "Denomination_Id" });
            DropIndex("dbo.CbType", new[] { "Variety_Id" });
            DropIndex("dbo.CbCoin", new[] { "Type_Id" });
            DropTable("dbo.CbCountry");
            DropTable("dbo.CbDenomination");
            DropTable("dbo.CbVariety");
            DropTable("dbo.CbType");
            DropTable("dbo.CbCoin");
        }
    }
}
