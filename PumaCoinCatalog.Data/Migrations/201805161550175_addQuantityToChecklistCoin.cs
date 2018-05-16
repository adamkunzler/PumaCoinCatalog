namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addQuantityToChecklistCoin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CbChecklistCoin", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CbChecklistCoin", "Quantity");
        }
    }
}
