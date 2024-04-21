CREATE TABLE [dbo].[LeagueCup] (
    [HattrickId]       BIGINT         NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [Type]             TINYINT        NOT NULL,
    [SubType]          TINYINT        NULL,
    [SeriesLevel]      TINYINT        NULL,
    [CurrentRound]     TINYINT        NOT NULL,
    [RoundsLeft]       TINYINT        NOT NULL,
    [LeagueHattrickId] BIGINT         NOT NULL,
    CONSTRAINT [PK_LeagueCup] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_LeagueCup_League] FOREIGN KEY ([LeagueHattrickId]) REFERENCES [dbo].[League] ([HattrickId])
);


GO
CREATE NONCLUSTERED INDEX [IX_LeagueCup_LeagueHattrickId]
    ON [dbo].[LeagueCup]([LeagueHattrickId] ASC);

