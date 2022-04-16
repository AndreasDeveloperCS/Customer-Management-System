SET IDENTITY_INSERT dbo.tblOrder ON;
ALTER TABLE dbo.tblOrder NOCHECK CONSTRAINT ALL

PRINT 'ORDER script'
MERGE INTO [dbo].[tblOrder] AS [Target]
USING (VALUES
     (1, DATEADD(day, -1, GETDATE()), 2000, 1, 'ANDREAS', NULL)
    ,(2, DATEADD(day, -3, GETDATE()), 3000, 2, 'ANDREAS', NULL)
    ,(3, DATEADD(day, -3, GETDATE()), 4000, 3, 'ANDREAS', NULL)
    ,(4, DATEADD(day, -5, GETDATE()), 5000, 4, 'ANDREAS', NULL)
    ,(5, DATEADD(day, -7, GETDATE()), 6000, 5, 'ANDREAS', NULL)
) AS [Source] 
(
    [colOrderId], 
    [colOrderDateTime], 
    [colOrderTotalPrice],
    [colOrderCustomerId],
    [colUserCreated], 
    [colUserModified]
)
ON ([Target].[colOrderId] = [Source].[colOrderId])
WHEN NOT MATCHED BY TARGET THEN
    INSERT
    (
        [colOrderId],
        [colOrderDateTime],
        [colOrderTotalPrice], 
        [colOrderCustomerId],
        [colUserCreated],
        [colUserModified]
    )
    VALUES
    (
		[Source].[colOrderId], 
		[Source].[colOrderDateTime],
		[Source].[colOrderTotalPrice], 
		[Source].[colOrderCustomerId],
		[Source].[colUserCreated], 
		[Source].[colUserModified]
	)
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

DECLARE @mergeError int
, @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
    IF @mergeError != 0
        BEGIN
        PRINT 'ERROR OCCURRED IN MERGE FOR dbo.[tblOrder]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
        END
    ELSE
        BEGIN
        PRINT 'dbo.[tblOrder] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
    END
GO

SET IDENTITY_INSERT dbo.tblOrder OFF;
ALTER TABLE dbo.tblOrder WITH CHECK CHECK CONSTRAINT ALL;
