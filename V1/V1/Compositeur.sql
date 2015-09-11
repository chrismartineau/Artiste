CREATE TABLE [dbo].[Compositeur] (
    [CompositeurID] INT           IDENTITY (1, 1) NOT NULL,
    [Nom]           NVARCHAR (40) NOT NULL,
    CONSTRAINT [Compositeur_PK] PRIMARY KEY CLUSTERED ([CompositeurID] ASC)
);
