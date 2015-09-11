CREATE TABLE [dbo].[Instrument] (
    [InstrumentID] INT           IDENTITY (1, 1) NOT NULL,
    [Nom]          NVARCHAR (30) NOT NULL,
    [Type]         NVARCHAR (30) NOT NULL,
    CONSTRAINT [Instrument_PK] PRIMARY KEY CLUSTERED ([InstrumentID] ASC),
    CONSTRAINT [Nom_Instrument_Unique] UNIQUE (Nom)
);
