namespace PumaCoinCatalog.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storedProcGetChecklistValueSummary_funcCalculateCbChecklistValue : DbMigration
    {
        public override void Up()
        {
            Sql(@" IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'CalculateCbChecklistValue' AND ROUTINE_SCHEMA = 'dbo' AND ROUTINE_TYPE = 'FUNCTION')
 	EXEC ('DROP FUNCTION [dbo].[CalculateCbChecklistValue]')
 GO

 CREATE FUNCTION [dbo].[CalculateCbChecklistValue]
 (
     @ChecklistId INT
 )
 RETURNS DECIMAL(10,5)
 BEGIN

    DECLARE @FaceValue DECIMAL(10,5)
    SELECT @FaceValue = d.FaceValue
    FROM CbChecklist chk
        LEFT JOIN CbType t ON t.Id = chk.Type_Id
        LEFT JOIN CbVariety v ON v.Id = t.Variety_Id
        LEFT JOIN CbDenomination d ON d.Id = v.Denomination_Id
    WHERE chk.Id = @ChecklistId
            
    DECLARE @MeltValue DECIMAL(10,5)
    SELECT @MeltValue = t.MeltValue
    FROM CbChecklist chk
        LEFT JOIN CbType t ON t.Id = chk.Type_Id            
    WHERE chk.Id = @ChecklistId        
        
    -- setup the cursor
    DECLARE @CurrentId INT    
    
    DECLARE the_cursor CURSOR FAST_FORWARD 
        FOR SELECT Id FROM CbChecklistCoin WHERE Checklist_Id = @ChecklistId AND InCollection = 1 and ShouldExclude = 0

    OPEN the_cursor
    FETCH NEXT FROM the_cursor INTO @CurrentId
    
    DECLARE @ChecklistValue DECIMAL(10,5) = 0
    
    -- LOOP
    WHILE @@FETCH_STATUS = 0
    BEGIN
        
        DECLARE @EstimateValue DECIMAL(10,5)
        SELECT @EstimateValue = ValueEstimate FROM CbChecklistCoin WHERE Id = @CurrentId
        
        IF(@EstimateValue > 0)
            SET @ChecklistValue = @ChecklistValue + @EstimateValue
        ELSE IF (@MeltValue > 0 AND @MeltValue > @FaceValue)
            SET @ChecklistValue = @ChecklistValue + @MeltValue
        ELSE
            SET @ChecklistValue = @ChecklistValue + @FaceValue
        
        FETCH NEXT FROM the_cursor INTO @CurrentId
    END

    CLOSE the_cursor
    DEALLOCATE the_cursor

    RETURN @ChecklistValue    
END
GO");

            Sql(@" IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetChecklistValueSummary' AND ROUTINE_SCHEMA = 'dbo' AND ROUTINE_TYPE = 'PROCEDURE')
 	EXEC ('DROP PROCEDURE [dbo].[GetChecklistValueSummary]')
 GO
 CREATE PROCEDURE [dbo].[GetChecklistValueSummary] (
     @ChecklistId INT
 )
 AS
 BEGIN
    
    DECLARE @NumCoinsInChecklist INT
    SELECT @NumCoinsInChecklist = COUNT(*) FROM CbChecklistCoin WHERE Checklist_Id = @ChecklistId AND ShouldExclude = 0

    DECLARE @NumCoinsInCollection INT
    SELECT @NumCoinsInCollection = COUNT(*) FROM CbChecklistCoin WHERE Checklist_Id = @ChecklistId AND InCollection = 1 AND ShouldExclude = 0

    SELECT TOP 1
        tp.MeltValue AS CoinBullionValue
        ,d.FaceValue AS CoinFaceValue

        ,@NumCoinsInCollection * d.FaceValue AS FaceValueTotal
        ,@NumCoinsInCollection * tp.MeltValue AS BullionValueTotal
        ,ISNULL((
            SELECT SUM(ValueEstimate) FROM CbChecklistCoin WHERE Checklist_Id = @ChecklistId AND InCollection = 1 AND ShouldExclude = 0
        ), 0) AS EstimatedValueTotal
        ,dbo.CalculateCbChecklistValue(chk.Id) AS CollectionValueTotal

        ,@NumCoinsInChecklist AS TotalCoinsInChecklist
        ,@NumCoinsInCollection AS TotalCoinsCollected
        ,CAST((@NumCoinsInCollection / CAST(@NumCoinsInChecklist AS DECIMAL) * 100) AS INT) AS TotalCoinsPercentage
    FROM CbChecklistCoin coin
        LEFT JOIN CbChecklist chk ON chk.Id = coin.Checklist_Id
        LEFT JOIN CbType tp ON tp.Id = chk.Type_Id
        LEFT JOIN CbVariety v ON v.Id = tp.Variety_Id
        LEFT JOIN CbDenomination d ON d.Id = v.Denomination_Id
    WHERE 
        coin.Checklist_Id = @ChecklistId    

END
GO    ");
        }
        
        public override void Down()
        {
        }
    }
}
