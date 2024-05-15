CREATE TABLE [senior].[MatchTeamBooking] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Index]            INT            NOT NULL,
    [PlayerHattrickId] BIGINT         NOT NULL,
    [PlayerName]       NVARCHAR (256) NOT NULL,
    [Type]             INT            NOT NULL,
    [Minute]           INT            NOT NULL,
    [MatchPart]        INT            NOT NULL,
    [MatchTeamId]      INT            NOT NULL,
    CONSTRAINT [PK_MatchTeamBooking] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamBooking_MatchTeam] FOREIGN KEY ([MatchTeamId]) REFERENCES [senior].[MatchTeam] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamBooking_MatchTeamId]
    ON [senior].[MatchTeamBooking]([MatchTeamId] ASC);

