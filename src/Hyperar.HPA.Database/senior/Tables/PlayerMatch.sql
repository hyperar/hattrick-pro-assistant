CREATE TABLE [senior].[PlayerMatch] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Date]             DATETIME       NOT NULL,
    [MatchHattrickId]  BIGINT         NOT NULL,
    [Role]             INT            NOT NULL,
    [AverageRating]    DECIMAL (4, 1) NOT NULL,
    [EndOfMatchRating] DECIMAL (4, 1) NOT NULL,
    [CalculatedRating] NVARCHAR (50)  NOT NULL,
    [PlayerHattrickId] BIGINT         NOT NULL,
    CONSTRAINT [PK_PlayerMatch] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlayerMatch_Player] FOREIGN KEY ([PlayerHattrickId]) REFERENCES [senior].[Player] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PlayerMatch_PlayerHattrickId]
    ON [senior].[PlayerMatch]([PlayerHattrickId] ASC);

