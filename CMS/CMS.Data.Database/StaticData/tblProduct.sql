SET IDENTITY_INSERT dbo.tblProduct ON;
ALTER TABLE dbo.tblProduct NOCHECK CONSTRAINT ALL;
PRINT 'PRODUCTS TABLE IS BEING UPDATED'
MERGE INTO [dbo].[tblProduct] AS [Target]
USING (VALUES
	 (1, N'Monitor',  		500,  'per/item',  	'ANDREAS', NULL)
	,(2, N'Notebook', 		3000, 'per/item', 	'ANDREAS', NULL)
	,(3, N'Motherboard', 	1000, 'per/item',	'ANDREAS', NULL)
	,(4, N'Server Station', 5000, 'per/item',	'ANDREAS', NULL)
	,(5, N'Router', 		500,  'per/item',	'ANDREAS', NULL)
	,(6, N'Processor', 		1000, 'per/item',	'ANDREAS', NULL)
	,(7, N'RAM', 			6000, 'per/item',	'ANDREAS', NULL)
	,(8, N'SSD', 			500,  'per/item',	'ANDREAS', NULL)
	,(9, N'GPU',			2000, 'per/item',	'ANDREAS', NULL)
) AS [Source] 
(
		  [colProductId]
		, [colProductName]
		, [colProductPricePerUnit]
		, [colProductMeasuringUnit]
		, [colUserCreated]
		, [colUserModified]
)
ON ([Target].[colProductId] = [Source].[colProductId])
WHEN NOT MATCHED BY TARGET THEN
    INSERT
	(
		  [colProductId]
		, [colProductName]
		, [colProductPricePerUnit]
		, [colProductMeasuringUnit]
		, [colUserCreated]
		, [colUserModified]
	)
    VALUES
	(
		  [Source].[colProductId]
		, [Source].[colProductName]
		, [Source].[colProductPricePerUnit]
		, [Source].[colProductMeasuringUnit]
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
        PRINT 'ERROR OCCURRED IN MERGE FOR dbo.[tblProduct]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
        END
    ELSE
        BEGIN
        PRINT 'dbo.[tblProduct] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
    END
GO

SET IDENTITY_INSERT dbo.tblProduct OFF;
ALTER TABLE dbo.tblProduct WITH CHECK CHECK CONSTRAINT ALL;
