namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lengthenCbCoinDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CbCoin", "Details", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CbCoin", "Details", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
