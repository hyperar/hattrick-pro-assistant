CREATE TABLE [senior].[MatchArena] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [HattrickId]      BIGINT         NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [Attendance]      INT            NULL,
    [TerracesSold]    INT            NULL,
    [BasicSeatsSold]  INT            NULL,
    [RoofSeatsSold]   INT            NULL,
    [VipSeatsSold]    INT            NULL,
    [MatchHattrickId] BIGINT         NOT NULL,
    CONSTRAINT [PK_MatchArena] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchArena_Match] FOREIGN KEY ([MatchHattrickId]) REFERENCES [senior].[Match] ([HattrickId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MatchArena_MatchHattrickId]
    ON [senior].[MatchArena]([MatchHattrickId] ASC);

