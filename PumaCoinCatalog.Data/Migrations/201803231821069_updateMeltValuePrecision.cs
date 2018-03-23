namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMeltValuePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CbType", "MeltValue", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CbType", "MeltValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
