CREATE TABLE [dbo].[Manager] (
    [HattrickId]        BIGINT          NOT NULL,
    [UserName]          NVARCHAR (128)  NOT NULL,
    [SupporterTier]     TINYINT          NOT NULL,
    [CurrencyName]      NVARCHAR (64)   NOT NULL,
    [CurrencyRate]      DECIMAL (10, 5) NOT NULL,
    [AvatarBytes]       VARBINARY (MAX) NULL,
    [CountryHattrickId] BIGINT          NOT NULL,
    [UserId]            INT             NOT NULL,
    CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_Manager_Country] FOREIGN KEY ([CountryHattrickId]) REFERENCES [dbo].[Country] ([HattrickId]),
    CONSTRAINT [FK_Manager_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Manager_UserId]
    ON [dbo].[Manager]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Manager_CountryHattrickId]
    ON [dbo].[Manager]([CountryHattrickId] ASC);

