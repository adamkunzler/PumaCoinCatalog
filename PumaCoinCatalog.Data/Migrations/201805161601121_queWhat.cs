namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class queWhat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChecklistCoin", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChecklistCoin", "Quantity");
        }
    }
}
