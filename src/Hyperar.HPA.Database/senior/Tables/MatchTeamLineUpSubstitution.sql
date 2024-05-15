CREATE TABLE [senior].[MatchTeamLineUpSubstitution] (
    [Id]                  INT    IDENTITY (1, 1) NOT NULL,
    [OrderType]           INT    NOT NULL,
    [NewRole]             INT    NOT NULL,
    [NewRoleBehavior]     INT    NOT NULL,
    [Minute]              INT    NOT NULL,
    [MatchPart]           INT    NOT NULL,
    [InPlayerHattrickId]  BIGINT NOT NULL,
    [OutPlayerHattrickId] BIGINT NOT NULL,
    [MatchTeamLineUpId]   INT    NOT NULL,
    CONSTRAINT [PK_MatchTeamLineUpSubstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamLineUpSubstitution_MatchTeamLineUp] FOREIGN KEY ([MatchTeamLineUpId]) REFERENCES [senior].[MatchTeamLineUp] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamLineUpSubstitution_MatchTeamLineUpId]
    ON [senior].[MatchTeamLineUpSubstitution]([MatchTeamLineUpId] ASC);

