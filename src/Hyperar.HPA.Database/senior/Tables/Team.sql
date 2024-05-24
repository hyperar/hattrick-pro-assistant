CREATE TABLE [senior].[Team] (
    [HattrickId]        BIGINT          NOT NULL,
    [Name]              NVARCHAR (256)  NOT NULL,
    [ShortName]         NVARCHAR (256)  NOT NULL,
    [IsPrimary]         BIT             NOT NULL,
    [FoundedOn]         DATETIME        NOT NULL,
    [CoachPlayerId]     BIGINT          NOT NULL,
    [IsPlayingCup]      BIT             NOT NULL,
    [GlobalRanking]     INT             NOT NULL,
    [LeagueRanking]     INT             NOT NULL,
    [RegionRanking]     INT             NOT NULL,
    [PowerRanking]      INT             NOT NULL,
    [TeamRank]          INT             NOT NULL,
    [UndefeatedStreak]  INT             NOT NULL,
    [WinStreak]         INT             NOT NULL,
    [SeriesHattrickId]  BIGINT          NOT NULL,
    [SeriesName]        NVARCHAR (100)  NOT NULL,
    [SeriesDivision]    INT             NOT NULL,
    [LogoBytes]         VARBINARY (MAX) NULL,
    [HomeMatchKitBytes] VARBINARY (MAX) NOT NULL,
    [AwayMatchKitBytes] VARBINARY (MAX) NOT NULL,
    [LeagueHattrickId]  BIGINT          NOT NULL,
    [ManagerHattrickId] BIGINT          NOT NULL,
    [RegionHattrickId]  BIGINT          NOT NULL,
    CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Senior_Team_League] FOREIGN KEY ([LeagueHattrickId]) REFERENCES [dbo].[League] ([HattrickId]),
    CONSTRAINT [FK_Senior_Team_Manager] FOREIGN KEY ([ManagerHattrickId]) REFERENCES [dbo].[Manager] ([HattrickId]),
    CONSTRAINT [FK_Senior_Team_Region] FOREIGN KEY ([RegionHattrickId]) REFERENCES [dbo].[Region] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_Team_RegionHattrickId]
    ON [senior].[Team]([RegionHattrickId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Team_ManagerHattrickId]
    ON [senior].[Team]([ManagerHattrickId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Team_LeagueHattrickId]
    ON [senior].[Team]([LeagueHattrickId] ASC);

