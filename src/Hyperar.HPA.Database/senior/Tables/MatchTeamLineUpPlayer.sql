CREATE TABLE [senior].[MatchTeamLineUpPlayer] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [HattrickId]        BIGINT         NOT NULL,
    [FirstName]         NVARCHAR (256) NOT NULL,
    [NickName]          NVARCHAR (256) NULL,
    [LastName]          NVARCHAR (256) NOT NULL,
    [Role]              INT            NOT NULL,
    [Behavior]          INT            NULL,
    [Rating]            DECIMAL (3, 1) NULL,
    [EndRating]         DECIMAL (3, 1) NULL,
    [MatchTeamLineUpId] INT            NOT NULL,
    CONSTRAINT [PK_MatchTeamLineUpPlayer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeamLineUpPlayer_MatchTeamLineUp] FOREIGN KEY ([MatchTeamLineUpId]) REFERENCES [senior].[MatchTeamLineUp] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchTeamLineUpPlayer_MatchTeamLineUpId]
    ON [senior].[MatchTeamLineUpPlayer]([MatchTeamLineUpId] ASC);

