CREATE TABLE [dbo].[Achat] (
    [AchatID]             INT   IDENTITY (1, 1) NOT NULL,
    [Cout]                MONEY NULL,
    [AlbumID]             INT   NULL,
    [ReleveTransactionID] INT   NULL,
    [VersionID]           INT   NULL,
    CONSTRAINT [Achat_PK] PRIMARY KEY CLUSTERED ([AchatID] ASC),
    CONSTRAINT [Achat_ReleveTransaction_FK] FOREIGN KEY ([ReleveTransactionID]) REFERENCES [dbo].[ReleveTransaction] ([ReleveTransactionID]),
    CONSTRAINT [Achat_Version_FK] FOREIGN KEY ([VersionID]) REFERENCES [dbo].[Version] ([VersionID]),
    CONSTRAINT [Achat_Album_FK] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([AlbumID]),
    CONSTRAINT [Achat_Unique] UNIQUE (ReleveTransactionID, VersionID, AchatID)
);
