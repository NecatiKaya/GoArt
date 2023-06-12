SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE TABLE [dbo].[Wallets](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Wallets] ADD  CONSTRAINT [PK_Wallets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MoneyTransactions](
	[Id] [uniqueidentifier] NOT NULL,
	[WalletId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Amount_Whole_Part] [int] NOT NULL,
	[Amount_Penny_Part] [int] NOT NULL,
	[Currency] [varchar](5) NOT NULL,
	[TransactionType] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MoneyTransactions] ADD  CONSTRAINT [PK_MoneyTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MoneyTransactions] ADD  CONSTRAINT [DEFAULT_MoneyTransactions_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[MoneyTransactions] ADD  CONSTRAINT [DEFAULT_MoneyTransactions_TransactionDate]  DEFAULT (getdate()) FOR [TransactionDate]
GO
