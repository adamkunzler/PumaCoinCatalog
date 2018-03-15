namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateImageProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CbType", "ObverseImageUri", c => c.String());
            AddColumn("dbo.CbType", "ReverseImageUri", c => c.String());
            AddColumn("dbo.CbVariety", "ObverseImageUri", c => c.String());
            AddColumn("dbo.CbVariety", "ReverseImageUri", c => c.String());
            DropColumn("dbo.CbType", "ImageUri");
            DropColumn("dbo.CbVariety", "ImageUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CbVariety", "ImageUri", c => c.String());
            AddColumn("dbo.CbType", "ImageUri", c => c.String());
            DropColumn("dbo.CbVariety", "ReverseImageUri");
            DropColumn("dbo.CbVariety", "ObverseImageUri");
            DropColumn("dbo.CbType", "ReverseImageUri");
            DropColumn("dbo.CbType", "ObverseImageUri");
        }
    }
}
