CREATE TABLE [dbo].[Chanson] (
    [ChansonID]    INT           IDENTITY (1, 1) NOT NULL,
    [DateCreation] DATETIME      NULL,
    [Titre]        NVARCHAR (30) NOT NULL,
	[GenreID]		INT NULL,
    CONSTRAINT [Titre_Chanson_Unique] UNIQUE (Titre),
    CONSTRAINT [Chanson_PK] PRIMARY KEY CLUSTERED ([ChansonID] ASC),
	CONSTRAINT [Chanson_Genre_FK] Foreign Key (GenreID) REFERENCES [dbo].[Genre] (GenreID)
);
