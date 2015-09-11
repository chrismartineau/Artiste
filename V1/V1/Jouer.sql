CREATE TABLE [dbo].[Jouer] (
	[JouerID]		INT IDENTITY(1,1) NOT NULL,
	[ArtisteID]    INT NULL,
	[InstrumentID] INT NULL,
	[VersionID]    INT NULL,
	CONSTRAINT [Jouer_Artiste_FK] FOREIGN KEY ([ArtisteID]) REFERENCES [dbo].[Artiste] ([ArtisteID]),
	CONSTRAINT [Jouer_Instrument_FK] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instrument] ([InstrumentID]),
	CONSTRAINT [Jouer_Version_FK] FOREIGN KEY ([VersionID]) REFERENCES [dbo].[Version] ([VersionID]),
	CONSTRAINT [Jouer_Unique] UNIQUE (ArtisteID, InstrumentID, VersionID),
	CONSTRAINT [Jouer_PK] Primary Key (JouerID)
);
