CREATE TABLE [dbo].[Genre] (
    [GenreID] INT           IDENTITY (1, 1) NOT NULL,
    [Nom]     NVARCHAR (30) NOT NULL,
    CONSTRAINT [Genre_PK] PRIMARY KEY CLUSTERED ([GenreID] ASC),
    CONSTRAINT [GenreNom_Unique] UNIQUE (Nom)
);
