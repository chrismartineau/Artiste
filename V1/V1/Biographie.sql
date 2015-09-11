CREATE TABLE [dbo].[Biographie] (
	[BiographieID]			INT		IDENTITY(1,1) NOT NULL,
    [Biographie]            NVARCHAR (MAX) NULL,
    [DateDernierChangement] DATETIME       NULL,
    [ArtisteID]             INT            NOT NULL,
    CONSTRAINT [Artiste_Biographie_FK] FOREIGN KEY ([ArtisteID]) REFERENCES [dbo].[Artiste] ([ArtisteID]) ON DELETE CASCADE,
    CONSTRAINT [Biographie_PK] PRIMARY KEY CLUSTERED ([BiographieID] ASC),
);
