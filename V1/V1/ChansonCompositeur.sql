CREATE TABLE [dbo].[ChansonCompositeur] (
    [CompositeurID] INT NOT NULL,
    [ChansonID]     INT NOT NULL,
    CONSTRAINT [CC_Compositeur_FK] FOREIGN KEY ([CompositeurID]) REFERENCES [dbo].[Compositeur] ([CompositeurID]),
    CONSTRAINT [CC_Chanson_FK] FOREIGN KEY ([ChansonID]) REFERENCES [dbo].[Chanson] ([ChansonID]),
    CONSTRAINT [ChansonCompositeur_Unique] UNIQUE (CompositeurID, ChansonID),
	CONSTRAINT [ChansonCompositeur_PK] PRIMARY KEY CLUSTERED ([CompositeurID], [ChansonID]),
);
