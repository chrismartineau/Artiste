CREATE TABLE [dbo].[Album] (
    [AlbumID]      INT            IDENTITY (1, 1) NOT NULL,
    [DateCreation] DATETIME       NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Prix]         MONEY          NULL,
    [Nom]          NVARCHAR (30)  NOT NULL,
    CONSTRAINT [Album_PK] PRIMARY KEY CLUSTERED ([AlbumID] ASC),
    CONSTRAINT [Nom_Album_Unique] UNIQUE NONCLUSTERED ([Nom] ASC)
);
