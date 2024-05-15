CREATE TABLE [dbo].[LeagueCup] (
    [HattrickId]       BIGINT         NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [Type]             INT            NOT NULL,
    [SubType]          INT            NULL,
    [SeriesLevel]      INT            NULL,
    [CurrentRound]     INT            NOT NULL,
    [RoundsLeft]       INT            NOT NULL,
    [LeagueHattrickId] BIGINT         NOT NULL,
    CONSTRAINT [PK_LeagueCup] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_LeagueCup_League] FOREIGN KEY ([LeagueHattrickId]) REFERENCES [dbo].[League] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_LeagueCup_LeagueHattrickId]
    ON [dbo].[LeagueCup]([LeagueHattrickId] ASC);

