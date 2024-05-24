CREATE TABLE [senior].[Player] (
    [HattrickId]                 BIGINT          NOT NULL,
    [FirstName]                  NVARCHAR (256)  NOT NULL,
    [NickName]                   NVARCHAR (256)  NULL,
    [LastName]                   NVARCHAR (256)  NOT NULL,
    [ShirtNumber]                INT             NULL,
    [IsCoach]                    BIT             NOT NULL,
    [AgeYears]                   INT             NOT NULL,
    [AgeDays]                    INT             NOT NULL,
    [JoinedTeamOn]               DATETIME        NOT NULL,
    [Notes]                      NVARCHAR (1024) NULL,
    [Statement]                  NVARCHAR (1024) NULL,
    [TotalSkillIndex]            INT             NOT NULL,
    [HasMotherClubBonus]         BIT             NOT NULL,
    [Salary]                     BIGINT          NOT NULL,
    [IsForeign]                  BIT             NOT NULL,
    [Agreeability]               INT             NOT NULL,
    [Aggressiveness]             INT             NOT NULL,
    [Honesty]                    INT             NOT NULL,
    [Leadership]                 INT             NOT NULL,
    [Specialty]                  INT             NOT NULL,
    [IsTransferListed]           BIT             NOT NULL,
    [EnrolledOnNationalTeam]     BIT             NOT NULL,
    [CurrentSeasonLeagueGoals]   INT             NOT NULL,
    [CurrentSeasonCupGoals]      INT             NOT NULL,
    [CurrentSeasonFriendlyGoals] INT             NOT NULL,
    [CareerGoals]                INT             NOT NULL,
    [CareerHattricks]            INT             NOT NULL,
    [GoalsOnTeam]                INT             NOT NULL,
    [MatchesOnTeam]              INT             NOT NULL,
    [SeniorNationalTeamCaps]     INT             NOT NULL,
    [JuniorNationalTeamCaps]     INT             NOT NULL,
    [BookingStatus]              INT             NOT NULL,
    [Health]                     INT             NOT NULL,
    [Category]                   INT             NOT NULL,
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

