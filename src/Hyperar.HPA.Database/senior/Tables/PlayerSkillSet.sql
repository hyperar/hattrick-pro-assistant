CREATE TABLE [senior].[PlayerSkillSet] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [Season]           SMALLINT NOT NULL,
    [Week]             TINYINT  NOT NULL,
    [Form]             TINYINT  NOT NULL,
    [Stamina]          TINYINT  NOT NULL,
    [Keeper]           TINYINT  NOT NULL,
    [Defending]        TINYINT  NOT NULL,
    [Playmaking]       TINYINT  NOT NULL,
    [Winger]           TINYINT  NOT NULL,
    [Passing]          TINYINT  NOT NULL,
    [Scoring]          TINYINT  NOT NULL,
    [SetPieces]        TINYINT  NOT NULL,
    [Loyalty]          TINYINT  NOT NULL,
    [Experience]       TINYINT  NOT NULL,
    [PlayerHattrickId] BIGINT   NOT NULL,
    CONSTRAINT [PK_PlayerSkillSet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlayerSkillSet_Player] FOREIGN KEY ([PlayerHattrickId]) REFERENCES [senior].[Player] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PlayerSkillSet_PlayerHattrickId]
    ON [senior].[PlayerSkillSet]([PlayerHattrickId] ASC);

