namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBullionValueToCoinType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrapeCoinType", "BullionValue", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrapeCoinType", "BullionValue");
        }
    }
}
