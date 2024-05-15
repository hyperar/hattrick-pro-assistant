CREATE TABLE [senior].[HallOfFamePlayer] (
    [HattrickId]        BIGINT         NOT NULL,
    [FirstName]         NVARCHAR (256) NOT NULL,
    [NickName]          NVARCHAR (256) NULL,
    [LastName]          NVARCHAR (256) NOT NULL,
    [Age]               INT            NOT NULL,
    [JoinedTeamOn]      DATETIME       NOT NULL,
    [NextBirthday]      DATETIME       NOT NULL,
    [IntroducedOn]      DATETIME       NOT NULL,
    [ExpertType]        INT            NOT NULL,
    [CountryHattrickId] BIGINT         NOT NULL,
    [TeamHattrickId]    BIGINT         NOT NULL,
    CONSTRAINT [PK_HallOfFamePlayer] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Senior_HallOfFamePlayer_Country] FOREIGN KEY ([CountryHattrickId]) REFERENCES [dbo].[Country] ([HattrickId]),
    CONSTRAINT [FK_Senior_HallOfFamePlayer_Team] FOREIGN KEY ([TeamHattrickId]) REFERENCES [senior].[Team] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_HallOfFamePlayer_CountryHattrickId]
    ON [senior].[HallOfFamePlayer]([CountryHattrickId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_HallOfFamePlayer_TeamHattrickId]
    ON [senior].[HallOfFamePlayer]([TeamHattrickId] ASC);