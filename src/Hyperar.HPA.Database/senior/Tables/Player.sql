CREATE TABLE [senior].[Player] (
    [HattrickId]                 BIGINT          NOT NULL,
    [FirstName]                  NVARCHAR (256)  NOT NULL,
    [NickName]                   NVARCHAR (256)  NULL,
    [LastName]                   NVARCHAR (256)  NOT NULL,
    [ShirtNumber]                TINYINT         NULL,
    [IsCoach]                    BIT             NOT NULL,
    [AgeYears]                   TINYINT         NOT NULL,
    [AgeDays]                    TINYINT         NOT NULL,
    [JoinedTeamOn]               DATETIME        NOT NULL,
    [Notes]                      NVARCHAR(1024)  NULL,
    [Statement]                  NVARCHAR(1024)  NULL,
    [TotalSkillIndex]            INT             NOT NULL,
    [HasMotherClubBonus]         BIT             NOT NULL,
    [Salary]                     BIGINT          NOT NULL,
    [IsForeign]                  BIT             NOT NULL,
    [Agreeability]               TINYINT         NOT NULL,
    [Aggressiveness]             TINYINT         NOT NULL,
    [Honesty]                    TINYINT         NOT NULL,
    [Leadership]                 TINYINT         NOT NULL,
    [Specialty]                  TINYINT         NOT NULL,
    [IsTransferListed]           BIT             NOT NULL,
    [EnrolledOnNationalTeam]     BIT             NOT NULL,
    [CurrentSeasonLeagueGoals]   SMALLINT        NOT NULL,
    [CurrentSeasonCupGoals]      SMALLINT        NOT NULL,
    [CurrentSeasonFriendlyGoals] SMALLINT        NOT NULL,
    [CareerGoals]                SMALLINT        NOT NULL,
    [CareerHattricks]            SMALLINT        NOT NULL,
    [GoalsOnTeam]                SMALLINT        NOT NULL,
    [MatchesOnTeam]              SMALLINT        NOT NULL,
    [SeniorNationalTeamCaps]     SMALLINT        NOT NULL,
    [JuniorNationalTeamCaps]     SMALLINT        NOT NULL,
    [BookingStatus]              TINYINT         NOT NULL,
    [Health]                     SMALLINT        NOT NULL,
    [Category]                   TINYINT         NOT NULL,
    [AvatarBytes]                VARBINARY (MAX) NULL,
    [CountryHattrickId]          BIGINT          NOT NULL,
    [TeamHattrickId]             BIGINT          NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Senior_Player_Country] FOREIGN KEY ([CountryHattrickId]) REFERENCES [dbo].[Country] ([HattrickId]),
    CONSTRAINT [FK_Senior_Player_Team] FOREIGN KEY ([TeamHattrickId]) REFERENCES [senior].[Team] ([HattrickId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Player_TeamHattrickId]
    ON [senior].[Player]([TeamHattrickId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Player_CountryHattrickId]
    ON [senior].[Player]([CountryHattrickId] ASC);

