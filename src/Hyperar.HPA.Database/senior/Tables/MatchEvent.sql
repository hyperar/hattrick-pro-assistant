CREATE TABLE [senior].[MatchEvent] (
    [Id]                         INT    IDENTITY (1, 1) NOT NULL,
    [Index]                      INT    NOT NULL,
    [PlayerHattrickId]           BIGINT NULL,
    [AdditionalPlayerHattrickId] BIGINT NULL,
    [TeamHattrickId]             BIGINT NULL,
    [Type]                       INT    NOT NULL,
    [Variation]                  INT    NOT NULL,
    [Text]                       NTEXT  NULL,
    [Minute]                     INT    NOT NULL,
    [MatchPart]                  INT    NOT NULL,
    [MatchHattrickId]            BIGINT NOT NULL,
    CONSTRAINT [PK_MatchEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchEvent_Match] FOREIGN KEY ([MatchHattrickId]) REFERENCES [senior].[Match] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchEvent_MatchHattrickId]
    ON [senior].[MatchEvent]([MatchHattrickId] ASC);

