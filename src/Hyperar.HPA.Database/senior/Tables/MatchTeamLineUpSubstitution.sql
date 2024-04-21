CREATE TABLE [senior].[MatchTeamLineUpSubstitution] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [OrderType]           TINYINT  NOT NULL,
    [NewRole]             SMALLINT NOT NULL,
    [NewRoleBehavior]     SMALLINT NOT NULL,
    [Minute]              TINYINT  NOT NULL,
    [MatchPart]           TINYINT  NOT NULL,
    [InPlayerHattrickId]  BIGINT   NOT NULL,
    [OutPlayerHattrickId] BIGINT   NOT NULL,
    [MatchTeamLineUpId]   INT      NOT NULL,
    CONSTRAINT [PK_MatchTeamLineUpSubstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamLineUpSubstitution_MatchTeamLineUp] FOREIGN KEY ([MatchTeamLineUpId]) REFERENCES [senior].[MatchTeamLineUp] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamLineUpSubstitution_MatchTeamLineUpId]
    ON [senior].[MatchTeamLineUpSubstitution]([MatchTeamLineUpId] ASC);

