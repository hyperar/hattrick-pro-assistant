CREATE TABLE [senior].[MatchTeamLineUp] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Experience]  INT NOT NULL,
    [Style]       INT NOT NULL,
    [MatchTeamId] INT NOT NULL,
    CONSTRAINT [PK_MatchTeamLineUp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamLineUp_MatchTeam] FOREIGN KEY ([MatchTeamId]) REFERENCES [senior].[MatchTeam] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MatchTeamLineUp_MatchTeamId]
    ON [senior].[MatchTeamLineUp]([MatchTeamId] ASC);

