namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storedProcGetCollectionDetails : DbMigration
    {
        public override void Up()
        {
            Sql(@"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetCollectionDetails' AND ROUTINE_SCHEMA = 'dbo' AND ROUTINE_TYPE = 'PROCEDURE')
	EXEC ('DROP PROCEDURE [dbo].[GetCollectionDetails]')
GO
CREATE PROCEDURE [dbo].[GetCollectionDetails] (
    @CollectionId INT
)
AS
BEGIN

    SELECT
        chk.Id
        ,chk.Title
        ,tp.Id
        ,tp.BeginDate
        ,tp.EndDate
        ,tp.ObverseImageUri
        ,tp.ReverseImageUri
    FROM CbChecklist chk
        LEFT JOIN CbType tp ON tp.Id = chk.Type_Id
        LEFT JOIN CbVariety v ON v.Id = tp.Variety_Id
        LEFT JOIN CbDenomination d ON d.Id = v.Denomination_Id
    WHERE 
        chk.Collection_Id = @CollectionId
    ORDER BY
        d.FaceValue
        ,tp.BeginDate

END
GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetCollectionDetails' AND ROUTINE_SCHEMA = 'dbo' AND ROUTINE_TYPE = 'PROCEDURE')
	EXEC ('DROP PROCEDURE [dbo].[GetCollectionDetails]')
GO");
        }
    }
}
