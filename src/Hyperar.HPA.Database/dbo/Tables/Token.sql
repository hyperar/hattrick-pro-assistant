CREATE TABLE [dbo].[Token] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Scope]       INT            NOT NULL,
    [Value]       NVARCHAR (128) NOT NULL,
    [SecretValue] NVARCHAR (128) NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [ExpiresOn]   DATETIME       NOT NULL,
    [UserId]      INT            NOT NULL,
    CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Token_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Token_UserId]
    ON [dbo].[Token]([UserId] ASC);

