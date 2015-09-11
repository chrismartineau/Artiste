CREATE TABLE [dbo].[ReleveTransaction] (
    [ReleveTransactionID] INT           IDENTITY (1, 1) NOT NULL,
    [Acheteur]            NVARCHAR (30) NULL,
    [CoutTotal]           NVARCHAR (30) NOT NULL,
    [Date]                DATETIME      NULL,
    CONSTRAINT [RELEVETRANSACTION_PK] PRIMARY KEY CLUSTERED ([ReleveTransactionID] ASC),
    CONSTRAINT [RELEVETRANSACTION_UNIQUE] UNIQUE (Acheteur, CoutTotal, [Date])
);
