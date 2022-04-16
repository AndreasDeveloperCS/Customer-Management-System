CREATE TABLE [dbo].[tblOrder]
(
	[colOrderId] [int] NOT NULL IDENTITY(1, 1),
	[colOrderDateTime] [datetime2] NOT NULL DEFAULT GETUTCDATE(),
	[colOrderTotalPrice] DOUBLE PRECISION NOT NULL,
	[colOrderCustomerId] [int] NOT NULL,
	[colDateCreated] [datetime2]  DEFAULT GETUTCDATE() NOT NULL,
	[colUserCreated] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colDateModified] [datetime2]  DEFAULT GETUTCDATE(),
	[colUserModified] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED  ([colOrderId]) ON [PRIMARY],
	CONSTRAINT [FK_tblOrder_colCustomer] FOREIGN KEY ([colOrderCustomerId]) REFERENCES [dbo].[tblCustomer] ([colCustomerId])
) ON [PRIMARY]
GO