CREATE TABLE [dbo].[Artiste] (
    [ArtisteID] INT           IDENTITY (1, 1) NOT NULL,
    [Nom]       NVARCHAR (40) NULL,
    [Role]      NVARCHAR (40) NULL,
    CONSTRAINT [Artiste_PK] PRIMARY KEY CLUSTERED ([ArtisteID] ASC)
);
