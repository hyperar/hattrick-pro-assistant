﻿CREATE TABLE [dbo].[League] (
    [HattrickId]           BIGINT          NOT NULL,
    [Name]                 NVARCHAR (256)  NOT NULL,
    [ShortName]            NVARCHAR (256)  NOT NULL,
    [EnglishName]          NVARCHAR (256)  NOT NULL,
    [Continent]            NVARCHAR (256)  NOT NULL,
    [Zone]                 NVARCHAR (256)  NOT NULL,
    [Season]               TINYINT         NOT NULL,
    [Week]                 TINYINT         NOT NULL,
    [SeasonOffset]         SMALLINT        NOT NULL,
    [LanguageId]           BIGINT          NOT NULL,
    [LanguageName]         NVARCHAR (256)  NOT NULL,
    [SeniorNationalTeamId] BIGINT          NOT NULL,
    [JuniorNationalTeamId] BIGINT          NOT NULL,
    [ActiveTeams]          INT             NOT NULL,
    [ActiveUsers]          INT             NOT NULL,
    [WaitingUsers]         INT             NOT NULL,
    [NumberOfLevels]       TINYINT         NOT NULL,
    [NextTrainingUpdate]   DATETIME        NOT NULL,
    [NextEconomyUpdate]    DATETIME        NOT NULL,
    [NextCupMatchDate]     DATETIME        NULL,
    [NextSeriesMatchDate]  DATETIME        NOT NULL,
    [FlagBytes]            VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_League] PRIMARY KEY CLUSTERED ([HattrickId] ASC)
);


