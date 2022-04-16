CREATE TABLE [dbo].[tblItem]
(
	[colItemId] [int] NOT NULL IDENTITY(1, 1),
	[colItemQuantity] decimal DEFAULT 0 NOT NULL,
	[colItemProductId] [int] NOT NULL,
	[colItemOrderId] [int] NOT NULL,
	[colDateCreated] [datetime2]  DEFAULT GETUTCDATE() NOT NULL,
	[colUserCreated] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colDateModified] [datetime2] DEFAULT GETUTCDATE(),
	[colUserModified] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT [PK_tblItem] PRIMARY KEY CLUSTERED  ([colItemId]) ON [PRIMARY],
	CONSTRAINT [FK_tblItem_colOrder] FOREIGN KEY ([colItemOrderId]) REFERENCES [dbo].[tblOrder] ([colOrderId]),
	CONSTRAINT [FK_tblItem_colProduct] FOREIGN KEY ([colItemProductId]) REFERENCES [dbo].[tblProduct] ([colProductId])
) ON [PRIMARY]
GO

