/*
Script de déploiement pour V1

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "V1"
:setvar DefaultFilePrefix "V1"
:setvar DefaultDataPath "C:\Users\Christophe\AppData\Local\Microsoft\VisualStudio\SSDT\V1"
:setvar DefaultLogPath "C:\Users\Christophe\AppData\Local\Microsoft\VisualStudio\SSDT\V1"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Suppression de [dbo].[Version_Album_FK]...';


GO
ALTER TABLE [dbo].[Version] DROP CONSTRAINT [Version_Album_FK];


GO
PRINT N'Modification de [dbo].[Version]...';


GO
ALTER TABLE [dbo].[Version] ALTER COLUMN [AlbumID] INT NULL;


GO
PRINT N'Création de [dbo].[Version_Album_FK]...';


GO
ALTER TABLE [dbo].[Version] WITH NOCHECK
    ADD CONSTRAINT [Version_Album_FK] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([AlbumID]) ON DELETE CASCADE;


GO
PRINT N'Vérification de données existantes par rapport aux nouvelles contraintes';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Version] WITH CHECK CHECK CONSTRAINT [Version_Album_FK];


GO
PRINT N'Mise à jour terminée.';


GO
