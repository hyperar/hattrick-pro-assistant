CREATE TABLE [senior].[MatchTeamGoal] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Index]            TINYINT        NOT NULL,
    [PlayerHattrickId] BIGINT         NOT NULL,
    [PlayerName]       NVARCHAR (256) NOT NULL,
    [HomeTeamScore]    TINYINT        NOT NULL,
    [AwayTeamScore]    TINYINT        NOT NULL,
    [Minute]           TINYINT        NOT NULL,
    [MatchPart]        TINYINT        NOT NULL,
    [MatchTeamId]      INT            NOT NULL,
    CONSTRAINT [PK_MatchTeamGoal] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamGoal_MatchTeam] FOREIGN KEY ([MatchTeamId]) REFERENCES [senior].[MatchTeam] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamGoal_MatchTeamId]
    ON [senior].[MatchTeamGoal]([MatchTeamId] ASC);

