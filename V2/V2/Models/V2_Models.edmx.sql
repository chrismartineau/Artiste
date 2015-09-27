
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2015 15:55:45
-- Generated from EDMX file: C:\Users\Christophe\Desktop\École\École\Troisième Année\Première Session\Projet de fin d'études\Artiste\V2\V2\Models\V2_Models.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [V2_bd];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Achat_Album_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Achat] DROP CONSTRAINT [FK_Achat_Album_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Achat_ReleveTransaction_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Achat] DROP CONSTRAINT [FK_Achat_ReleveTransaction_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Achat_Version_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Achat] DROP CONSTRAINT [FK_Achat_Version_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Artiste_Biographie_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Biographie] DROP CONSTRAINT [FK_Artiste_Biographie_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Chanson_Genre_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chanson] DROP CONSTRAINT [FK_Chanson_Genre_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_ChansonCompositeur_Chanson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChansonCompositeur] DROP CONSTRAINT [FK_ChansonCompositeur_Chanson];
GO
IF OBJECT_ID(N'[dbo].[FK_ChansonCompositeur_Compositeur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChansonCompositeur] DROP CONSTRAINT [FK_ChansonCompositeur_Compositeur];
GO
IF OBJECT_ID(N'[dbo].[FK_Jouer_Artiste_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_Jouer_Artiste_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Jouer_Instrument_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_Jouer_Instrument_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Jouer_Version_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_Jouer_Version_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Version_Album_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Version] DROP CONSTRAINT [FK_Version_Album_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_Version_Chanson_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Version] DROP CONSTRAINT [FK_Version_Chanson_FK];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Achat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Achat];
GO
IF OBJECT_ID(N'[dbo].[Album]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Album];
GO
IF OBJECT_ID(N'[dbo].[Artiste]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Artiste];
GO
IF OBJECT_ID(N'[dbo].[Biographie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Biographie];
GO
IF OBJECT_ID(N'[dbo].[Chanson]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chanson];
GO
IF OBJECT_ID(N'[dbo].[ChansonCompositeur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChansonCompositeur];
GO
IF OBJECT_ID(N'[dbo].[Compositeur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Compositeur];
GO
IF OBJECT_ID(N'[dbo].[Genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genre];
GO
IF OBJECT_ID(N'[dbo].[Instrument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instrument];
GO
IF OBJECT_ID(N'[dbo].[Jouer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jouer];
GO
IF OBJECT_ID(N'[dbo].[ReleveTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReleveTransaction];
GO
IF OBJECT_ID(N'[dbo].[Version]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Version];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Achat'
CREATE TABLE [dbo].[Achat] (
    [AchatID] int IDENTITY(1,1) NOT NULL,
    [Cout] decimal(19,4)  NULL,
    [AlbumID] int  NULL,
    [ReleveTransactionID] int  NULL,
    [VersionID] int  NULL
);
GO

-- Creating table 'Album'
CREATE TABLE [dbo].[Album] (
    [AlbumID] int IDENTITY(1,1) NOT NULL,
    [DateCreation] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [Prix] decimal(19,4)  NULL,
    [Nom] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Artiste'
CREATE TABLE [dbo].[Artiste] (
    [ArtisteID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(40)  NULL,
    [Role] nvarchar(40)  NULL
);
GO

-- Creating table 'Biographie'
CREATE TABLE [dbo].[Biographie] (
    [BiographieID] int IDENTITY(1,1) NOT NULL,
    [Biographie1] nvarchar(max)  NULL,
    [DateDernierChangement] datetime  NULL,
    [ArtisteID] int  NOT NULL
);
GO

-- Creating table 'Chanson'
CREATE TABLE [dbo].[Chanson] (
    [ChansonID] int IDENTITY(1,1) NOT NULL,
    [DateCreation] datetime  NULL,
    [Titre] nvarchar(30)  NOT NULL,
    [GenreID] int  NULL
);
GO

-- Creating table 'Compositeur'
CREATE TABLE [dbo].[Compositeur] (
    [CompositeurID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'Genre'
CREATE TABLE [dbo].[Genre] (
    [GenreID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Instrument'
CREATE TABLE [dbo].[Instrument] (
    [InstrumentID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(30)  NOT NULL,
    [Type] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Jouer'
CREATE TABLE [dbo].[Jouer] (
    [JouerID] int IDENTITY(1,1) NOT NULL,
    [ArtisteID] int  NULL,
    [InstrumentID] int  NULL,
    [VersionID] int  NULL
);
GO

-- Creating table 'ReleveTransaction'
CREATE TABLE [dbo].[ReleveTransaction] (
    [ReleveTransactionID] int IDENTITY(1,1) NOT NULL,
    [Acheteur] nvarchar(30)  NULL,
    [CoutTotal] nvarchar(30)  NOT NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'Version'
CREATE TABLE [dbo].[Version] (
    [VersionID] int IDENTITY(1,1) NOT NULL,
    [ChansonID] int  NOT NULL,
    [AlbumID] int  NOT NULL,
    [Commentaire] nvarchar(50)  NULL,
    [DateCreation] datetime  NULL,
    [Demo] nvarchar(max)  NULL,
    [Duree] int  NULL,
    [NbEcoutes] int  NULL,
    [Prix] decimal(19,4)  NULL,
    [Visible] bit  NULL
);
GO

-- Creating table 'ChansonCompositeur'
CREATE TABLE [dbo].[ChansonCompositeur] (
    [Chanson_ChansonID] int  NOT NULL,
    [Compositeur_CompositeurID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AchatID] in table 'Achat'
ALTER TABLE [dbo].[Achat]
ADD CONSTRAINT [PK_Achat]
    PRIMARY KEY CLUSTERED ([AchatID] ASC);
GO

-- Creating primary key on [AlbumID] in table 'Album'
ALTER TABLE [dbo].[Album]
ADD CONSTRAINT [PK_Album]
    PRIMARY KEY CLUSTERED ([AlbumID] ASC);
GO

-- Creating primary key on [ArtisteID] in table 'Artiste'
ALTER TABLE [dbo].[Artiste]
ADD CONSTRAINT [PK_Artiste]
    PRIMARY KEY CLUSTERED ([ArtisteID] ASC);
GO

-- Creating primary key on [BiographieID] in table 'Biographie'
ALTER TABLE [dbo].[Biographie]
ADD CONSTRAINT [PK_Biographie]
    PRIMARY KEY CLUSTERED ([BiographieID] ASC);
GO

-- Creating primary key on [ChansonID] in table 'Chanson'
ALTER TABLE [dbo].[Chanson]
ADD CONSTRAINT [PK_Chanson]
    PRIMARY KEY CLUSTERED ([ChansonID] ASC);
GO

-- Creating primary key on [CompositeurID] in table 'Compositeur'
ALTER TABLE [dbo].[Compositeur]
ADD CONSTRAINT [PK_Compositeur]
    PRIMARY KEY CLUSTERED ([CompositeurID] ASC);
GO

-- Creating primary key on [GenreID] in table 'Genre'
ALTER TABLE [dbo].[Genre]
ADD CONSTRAINT [PK_Genre]
    PRIMARY KEY CLUSTERED ([GenreID] ASC);
GO

-- Creating primary key on [InstrumentID] in table 'Instrument'
ALTER TABLE [dbo].[Instrument]
ADD CONSTRAINT [PK_Instrument]
    PRIMARY KEY CLUSTERED ([InstrumentID] ASC);
GO

-- Creating primary key on [JouerID] in table 'Jouer'
ALTER TABLE [dbo].[Jouer]
ADD CONSTRAINT [PK_Jouer]
    PRIMARY KEY CLUSTERED ([JouerID] ASC);
GO

-- Creating primary key on [ReleveTransactionID] in table 'ReleveTransaction'
ALTER TABLE [dbo].[ReleveTransaction]
ADD CONSTRAINT [PK_ReleveTransaction]
    PRIMARY KEY CLUSTERED ([ReleveTransactionID] ASC);
GO

-- Creating primary key on [VersionID] in table 'Version'
ALTER TABLE [dbo].[Version]
ADD CONSTRAINT [PK_Version]
    PRIMARY KEY CLUSTERED ([VersionID] ASC);
GO

-- Creating primary key on [Chanson_ChansonID], [Compositeur_CompositeurID] in table 'ChansonCompositeur'
ALTER TABLE [dbo].[ChansonCompositeur]
ADD CONSTRAINT [PK_ChansonCompositeur]
    PRIMARY KEY CLUSTERED ([Chanson_ChansonID], [Compositeur_CompositeurID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AlbumID] in table 'Achat'
ALTER TABLE [dbo].[Achat]
ADD CONSTRAINT [FK_Achat_Album_FK]
    FOREIGN KEY ([AlbumID])
    REFERENCES [dbo].[Album]
        ([AlbumID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Achat_Album_FK'
CREATE INDEX [IX_FK_Achat_Album_FK]
ON [dbo].[Achat]
    ([AlbumID]);
GO

-- Creating foreign key on [ReleveTransactionID] in table 'Achat'
ALTER TABLE [dbo].[Achat]
ADD CONSTRAINT [FK_Achat_ReleveTransaction_FK]
    FOREIGN KEY ([ReleveTransactionID])
    REFERENCES [dbo].[ReleveTransaction]
        ([ReleveTransactionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Achat_ReleveTransaction_FK'
CREATE INDEX [IX_FK_Achat_ReleveTransaction_FK]
ON [dbo].[Achat]
    ([ReleveTransactionID]);
GO

-- Creating foreign key on [VersionID] in table 'Achat'
ALTER TABLE [dbo].[Achat]
ADD CONSTRAINT [FK_Achat_Version_FK]
    FOREIGN KEY ([VersionID])
    REFERENCES [dbo].[Version]
        ([VersionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Achat_Version_FK'
CREATE INDEX [IX_FK_Achat_Version_FK]
ON [dbo].[Achat]
    ([VersionID]);
GO

-- Creating foreign key on [AlbumID] in table 'Version'
ALTER TABLE [dbo].[Version]
ADD CONSTRAINT [FK_Version_Album_FK]
    FOREIGN KEY ([AlbumID])
    REFERENCES [dbo].[Album]
        ([AlbumID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Version_Album_FK'
CREATE INDEX [IX_FK_Version_Album_FK]
ON [dbo].[Version]
    ([AlbumID]);
GO

-- Creating foreign key on [ArtisteID] in table 'Biographie'
ALTER TABLE [dbo].[Biographie]
ADD CONSTRAINT [FK_Artiste_Biographie_FK]
    FOREIGN KEY ([ArtisteID])
    REFERENCES [dbo].[Artiste]
        ([ArtisteID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Artiste_Biographie_FK'
CREATE INDEX [IX_FK_Artiste_Biographie_FK]
ON [dbo].[Biographie]
    ([ArtisteID]);
GO

-- Creating foreign key on [ArtisteID] in table 'Jouer'
ALTER TABLE [dbo].[Jouer]
ADD CONSTRAINT [FK_Jouer_Artiste_FK]
    FOREIGN KEY ([ArtisteID])
    REFERENCES [dbo].[Artiste]
        ([ArtisteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Jouer_Artiste_FK'
CREATE INDEX [IX_FK_Jouer_Artiste_FK]
ON [dbo].[Jouer]
    ([ArtisteID]);
GO

-- Creating foreign key on [GenreID] in table 'Chanson'
ALTER TABLE [dbo].[Chanson]
ADD CONSTRAINT [FK_Chanson_Genre_FK]
    FOREIGN KEY ([GenreID])
    REFERENCES [dbo].[Genre]
        ([GenreID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Chanson_Genre_FK'
CREATE INDEX [IX_FK_Chanson_Genre_FK]
ON [dbo].[Chanson]
    ([GenreID]);
GO

-- Creating foreign key on [ChansonID] in table 'Version'
ALTER TABLE [dbo].[Version]
ADD CONSTRAINT [FK_Version_Chanson_FK]
    FOREIGN KEY ([ChansonID])
    REFERENCES [dbo].[Chanson]
        ([ChansonID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Version_Chanson_FK'
CREATE INDEX [IX_FK_Version_Chanson_FK]
ON [dbo].[Version]
    ([ChansonID]);
GO

-- Creating foreign key on [InstrumentID] in table 'Jouer'
ALTER TABLE [dbo].[Jouer]
ADD CONSTRAINT [FK_Jouer_Instrument_FK]
    FOREIGN KEY ([InstrumentID])
    REFERENCES [dbo].[Instrument]
        ([InstrumentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Jouer_Instrument_FK'
CREATE INDEX [IX_FK_Jouer_Instrument_FK]
ON [dbo].[Jouer]
    ([InstrumentID]);
GO

-- Creating foreign key on [VersionID] in table 'Jouer'
ALTER TABLE [dbo].[Jouer]
ADD CONSTRAINT [FK_Jouer_Version_FK]
    FOREIGN KEY ([VersionID])
    REFERENCES [dbo].[Version]
        ([VersionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Jouer_Version_FK'
CREATE INDEX [IX_FK_Jouer_Version_FK]
ON [dbo].[Jouer]
    ([VersionID]);
GO

-- Creating foreign key on [Chanson_ChansonID] in table 'ChansonCompositeur'
ALTER TABLE [dbo].[ChansonCompositeur]
ADD CONSTRAINT [FK_ChansonCompositeur_Chanson]
    FOREIGN KEY ([Chanson_ChansonID])
    REFERENCES [dbo].[Chanson]
        ([ChansonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Compositeur_CompositeurID] in table 'ChansonCompositeur'
ALTER TABLE [dbo].[ChansonCompositeur]
ADD CONSTRAINT [FK_ChansonCompositeur_Compositeur]
    FOREIGN KEY ([Compositeur_CompositeurID])
    REFERENCES [dbo].[Compositeur]
        ([CompositeurID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChansonCompositeur_Compositeur'
CREATE INDEX [IX_FK_ChansonCompositeur_Compositeur]
ON [dbo].[ChansonCompositeur]
    ([Compositeur_CompositeurID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------