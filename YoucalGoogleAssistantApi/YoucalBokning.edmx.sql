
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/04/2019 16:17:26
-- Generated from EDMX file: C:\Users\Haris\Source\Repos\YoucalGoogleAssistantApi2\YoucalGoogleAssistantApi\YoucalBokning.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Youcal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BookingSet'
CREATE TABLE [dbo].[BookingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [UserPhone] nvarchar(max)  NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [StartTime] nvarchar(max)  NOT NULL,
    [EndTime] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Company_Id] int  NOT NULL
);
GO

-- Creating table 'CompanySet'
CREATE TABLE [dbo].[CompanySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProvidesSet'
CREATE TABLE [dbo].[ProvidesSet] (
    [CompanyId] int  NOT NULL,
    [ServicesId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServicesSet'
CREATE TABLE [dbo].[ServicesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [PK_BookingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompanySet'
ALTER TABLE [dbo].[CompanySet]
ADD CONSTRAINT [PK_CompanySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CompanyId], [ServicesId] in table 'ProvidesSet'
ALTER TABLE [dbo].[ProvidesSet]
ADD CONSTRAINT [PK_ProvidesSet]
    PRIMARY KEY CLUSTERED ([CompanyId], [ServicesId] ASC);
GO

-- Creating primary key on [Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [PK_ServicesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Company_Id] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [FK_BookingCompany]
    FOREIGN KEY ([Company_Id])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingCompany'
CREATE INDEX [IX_FK_BookingCompany]
ON [dbo].[BookingSet]
    ([Company_Id]);
GO

-- Creating foreign key on [CompanyId] in table 'ProvidesSet'
ALTER TABLE [dbo].[ProvidesSet]
ADD CONSTRAINT [FK_CompanyProvides]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ServicesId] in table 'ProvidesSet'
ALTER TABLE [dbo].[ProvidesSet]
ADD CONSTRAINT [FK_ServicesProvides]
    FOREIGN KEY ([ServicesId])
    REFERENCES [dbo].[ServicesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServicesProvides'
CREATE INDEX [IX_FK_ServicesProvides]
ON [dbo].[ProvidesSet]
    ([ServicesId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------