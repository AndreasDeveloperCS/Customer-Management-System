SET IDENTITY_INSERT dbo.tblItem ON;
ALTER TABLE dbo.tblItem NOCHECK CONSTRAINT ALL;

MERGE INTO [dbo].[tblItem] AS [Target]
USING (VALUES
	 (1, 4, 1, 3, 'ANDREAS', NULL)
	,(2, 1, 2, 3, 'ANDREAS', NULL)
	,(3, 3, 3, 2, 'ANDREAS', NULL)
	,(4, 2, 4, 1, 'ANDREAS', NULL)
	,(5, 2, 5, 4, 'ANDREAS', NULL)
	,(6, 1, 6, 2, 'ANDREAS', NULL)
	,(7, 5, 7, 5, 'ANDREAS', NULL)
	,(8, 8, 8, 3, 'ANDREAS', NULL)
	,(9, 2, 9, 4, 'ANDREAS', NULL)
) AS [Source] 
(
		  [colItemId]
		, [colItemQuantity]
		, [colItemProductId] 
		, [colItemOrderId]
		, [colUserCreated]
		, [colUserModified]
)
ON ([Target].[colItemId] = [Source].[colItemId])
WHEN NOT MATCHED BY TARGET THEN
    INSERT
	(   
		  [colItemId]
		, [colItemQuantity]
		, [colItemProductId] 
		, [colItemOrderId]
		, [colUserCreated]
		, [colUserModified]
	)
    VALUES
	(
	      [colItemId]
		, [colItemQuantity]
		, [colItemProductId] 
		, [colItemOrderId]
		, [Source].[colUserCreated]
		, [Source].[colUserModified]
	)
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

DECLARE @mergeError int
	  , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
    IF @mergeError != 0
        BEGIN
        PRINT 'ERROR OCCURRED IN MERGE FOR dbo.[tblItem]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
        END
    ELSE
        BEGIN
        PRINT 'dbo.[tblItem] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
    END
GO

SET IDENTITY_INSERT dbo.tblItem OFF;
ALTER TABLE dbo.tblItem WITH CHECK CHECK CONSTRAINT ALL;
