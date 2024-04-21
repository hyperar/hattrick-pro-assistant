CREATE TABLE [senior].[MatchTeamLineUpStartingPlayer] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [HattrickId]        BIGINT         NOT NULL,
    [FirstName]         NVARCHAR (256) NOT NULL,
    [NickName]          NVARCHAR (256) NULL,
    [LastName]          NVARCHAR (256) NOT NULL,
    [Role]              SMALLINT       NOT NULL,
    [Behavior]          SMALLINT       NULL,
    [MatchTeamLineUpId] INT            NOT NULL,
    CONSTRAINT [PK_MatchTeamLineUpStartingPlayer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamLineUpStartingPlayer_MatchTeamLineUp] FOREIGN KEY ([MatchTeamLineUpId]) REFERENCES [senior].[MatchTeamLineUp] ([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_MatchTeamLineUpStartingPlayer_MatchTeamLineUpId]
    ON [senior].[MatchTeamLineUpStartingPlayer]([MatchTeamLineUpId] ASC);
GO

