CREATE TABLE [senior].[TeamArena] (
    [HattrickId]        BIGINT          NOT NULL,
    [Name]              NVARCHAR (256)  NOT NULL,
    [BuiltOn]           DATETIME        NOT NULL,
    [TerracesCapacity]  INT             NOT NULL,
    [BasicSeatCapacity] INT             NOT NULL,
    [RoofSeatCapacity]  INT             NOT NULL,
    [VipLoungeCapacity] INT             NOT NULL,
    [TotalCapacity]     INT             NOT NULL,
    [ImageBytes]        VARBINARY (MAX) NOT NULL,
    [TeamHattrickId]    BIGINT          NOT NULL,
    CONSTRAINT [PK_TeamArena] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Senior_TeamArena_Team] FOREIGN KEY ([TeamHattrickId]) REFERENCES [senior].[Team] ([HattrickId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TeamArena_TeamHattrickId]
    ON [senior].[TeamArena]([TeamHattrickId] ASC);

