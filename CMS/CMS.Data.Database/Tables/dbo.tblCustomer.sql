CREATE TABLE [dbo].[tblCustomer]
(
	[colCustomerId] [int] NOT NULL IDENTITY(1, 1),
	[colFirstName] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colLastName] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colAddress] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL DEFAULT '',
	[colPostalCode] [varchar] (16) COLLATE Latin1_General_CI_AS NOT NULL DEFAULT '',
	[colDateCreated] [datetime2]  DEFAULT GETUTCDATE() NOT NULL,
	[colUserCreated] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL,
	[colDateModified] [datetime2]  DEFAULT GETUTCDATE(),
	[colUserModified] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([colCustomerId]) ON [PRIMARY]
) ON [PRIMARY]
GO