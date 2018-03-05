namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExcludeAttributeToChecklistCoin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChecklistCoin", "ShouldExclude", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChecklistCoin", "ShouldExclude");
        }
    }
}
