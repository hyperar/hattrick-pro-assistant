CREATE TABLE [dbo].[Country] (
    [HattrickId]       BIGINT          NOT NULL,
    [Name]             NVARCHAR (256)  NOT NULL,
    [CurrencyName]     NVARCHAR (64)   NOT NULL,
    [CurrencyRate]     DECIMAL (10, 5) NOT NULL,
    [Code]             NVARCHAR (4)    NOT NULL,
    [DateFormat]       NVARCHAR (20)   NOT NULL,
    [TimeFormat]       NVARCHAR (20)   NOT NULL,
    [LeagueHattrickId] BIGINT          NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Country_League] FOREIGN KEY ([LeagueHattrickId]) REFERENCES [dbo].[League] ([HattrickId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Country_LeagueHattrickId]
    ON [dbo].[Country]([LeagueHattrickId] ASC);

