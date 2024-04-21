CREATE TABLE [senior].[Match] (
    [HattrickId]          BIGINT   NOT NULL,
    [System]              TINYINT  NOT NULL,
    [Type]                TINYINT  NOT NULL,
    [CompetitionId]       BIGINT   NULL,
    [Rules]               TINYINT  NOT NULL,
    [StartDate]           DATETIME NOT NULL, 
    [FinishDate]          DATETIME NULL,
    [AddedMinutes]        TINYINT  NULL,
    [Weather]             TINYINT  NULL,
    [TeamHattrickId]      BIGINT   NOT NULL,
    CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Senior_Match_Team] FOREIGN KEY ([TeamHattrickId]) REFERENCES [senior].[Team] ([HattrickId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Match_TeamHattrickId]
    ON [senior].[Match]([TeamHattrickId] ASC);

