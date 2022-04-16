
SET IDENTITY_INSERT dbo.tblCustomer ON;
ALTER TABLE dbo.tblCustomer NOCHECK CONSTRAINT ALL;

MERGE INTO [dbo].[tblCustomer] AS [Target]
USING (VALUES
     (1, N'Ilon',       N'Mask',        '01005',    'California',  'ANDREAS', NULL)
    ,(2, N'Vaios',      N'Vaitsis',     '01004',    'Athens',      'ANDREAS', NULL)
    ,(3, N'Andreas',    N'Petrov',      '01007',    'Athens',      'ANDREAS', NULL)
    ,(4, N'Adriano',    N'Chelentano',  '01008',    'Rome',        'ANDREAS', NULL)
    ,(5, N'Arnold',     N'Willis',      '01003',    'Florida',     'ANDREAS', NULL)
) AS [Source] 
(
    [colCustomerId], 
    [colFirstName], 
    [colLastName],
    [colPostalCode], 
    [colAddress], 
    [colUserCreated], 
    [colUserModified]
)
ON ([Target].[colCustomerId] = [Source].[colCustomerId])
WHEN NOT MATCHED BY TARGET THEN
    INSERT
    (
        [colCustomerId], 
        [colFirstName],
        [colLastName],
        [colAddress],
        [colPostalCode],
        [colUserCreated], 
        [colUserModified]
    )
    VALUES
    (
        [Source].[colCustomerId], 
        [Source].[colFirstName], 
        [Source].[colLastName],
        [Source].[colAddress], 
        [Source].[colPostalCode], 
        [Source].[colUserCreated],
        [Source].[colUserModified]
    )
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

DECLARE @mergeError int, @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
    IF @mergeError != 0
        BEGIN
        PRINT 'ERROR OCCURRED IN MERGE FOR dbo.[tblCustomer]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
        END
    ELSE
        BEGIN
        PRINT 'dbo.[tblCustomer] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
    END
GO
SET IDENTITY_INSERT dbo.tblCustomer OFF;
ALTER TABLE dbo.tblCustomer WITH CHECK CHECK CONSTRAINT ALL;
