CREATE TABLE [senior].[MatchOfficial] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [HattrickId]        BIGINT         NOT NULL,
    [Name]              NVARCHAR (256) NOT NULL,
    [CountryHattrickId] BIGINT         NOT NULL,
    [MatchHattrickId]   BIGINT         NOT NULL,
    CONSTRAINT [PK_MatchOfficial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchOfficial_Country] FOREIGN KEY ([CountryHattrickId]) REFERENCES [dbo].[Country] ([HattrickId]),
    CONSTRAINT [FK_Senior_MatchOfficial_Match] FOREIGN KEY ([MatchHattrickId]) REFERENCES [senior].[Match] ([HattrickId])
);


GO
CREATE NONCLUSTERED INDEX [IX_MatchOfficial_MatchHattrickId]
    ON [senior].[MatchOfficial]([MatchHattrickId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MatchOfficial_CountryHattrickId]
    ON [senior].[MatchOfficial]([CountryHattrickId] ASC);

