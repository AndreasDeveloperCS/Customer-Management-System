CREATE TABLE [dbo].[tblProduct]
(
	[colProductId] [int] NOT NULL IDENTITY(1, 1),
	[colProductName] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colProductPricePerUnit] decimal NOT NULL,
	[colProductMeasuringUnit] [nvarchar] (255) NOT NULL,
	[colDateCreated] [datetime2]  DEFAULT GETUTCDATE() NOT NULL,
	[colUserCreated] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colDateModified] [datetime2] DEFAULT GETUTCDATE(),
	[colUserModified] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED ([colProductId]) ON [PRIMARY]
) ON [PRIMARY]
GO

