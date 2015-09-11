CREATE TABLE [dbo].[Version] (
    [VersionID]    INT           IDENTITY (1, 1) NOT NULL,
    [ChansonID]    INT         NOT NULL,
    [AlbumID]      INT         NOT NULL,
    [Commentaire]  NVARCHAR (50) NULL,
    [DateCreation] DATETIME      NULL,
    [Demo]         VARBINARY(MAX) NULL,
    [Duree]        INT           NULL,
    [NbEcoutes]    INT           NULL,
    [Prix]         MONEY         NULL,
    [Visible]      BIT           NULL,
    CONSTRAINT [Version_PK] PRIMARY KEY CLUSTERED ([VersionID] ASC),
    CONSTRAINT [Version_Chanson_FK] FOREIGN KEY ([ChansonID]) REFERENCES [dbo].[Chanson] ([ChansonID]) ON DELETE CASCADE,
    CONSTRAINT [Version_Album_FK] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([AlbumID]) ON DELETE CASCADE
);
