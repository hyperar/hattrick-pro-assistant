CREATE TABLE [senior].[MatchTeamInjury] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Index]            INT            NOT NULL,
    [PlayerHattrickId] BIGINT         NOT NULL,
    [PlayerName]       NVARCHAR (256) NOT NULL,
    [Type]             INT            NOT NULL,
    [Minute]           INT            NOT NULL,
    [MatchPart]        INT            NOT NULL,
    [MatchTeamId]      INT            NOT NULL,
    CONSTRAINT [PK_MatchTeamInjury] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamInjury_MatchTeam] FOREIGN KEY ([MatchTeamId]) REFERENCES [senior].[MatchTeam] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamInjury_MatchTeamId]
    ON [senior].[MatchTeamInjury]([MatchTeamId] ASC);

