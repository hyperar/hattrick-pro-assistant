CREATE TABLE [dbo].[Region] (
    [HattrickId]        BIGINT         NOT NULL,
    [Name]              NVARCHAR (256) NOT NULL,
    [CountryHattrickId] BIGINT         NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Region_Country] FOREIGN KEY ([CountryHattrickId]) REFERENCES [dbo].[Country] ([HattrickId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Region_CountryHattrickId]
    ON [dbo].[Region]([CountryHattrickId] ASC);

