CREATE TABLE [dbo].[User] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [LastDownloadDate] DATETIME NULL,
    [LastSelectedTeamHattrickId] BIGINT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

