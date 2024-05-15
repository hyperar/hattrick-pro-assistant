CREATE TABLE [senior].[PlayerSkillSet] (
    [Id]               INT    IDENTITY (1, 1) NOT NULL,
    [Season]           INT    NOT NULL,
    [Week]             INT    NOT NULL,
    [Form]             INT    NOT NULL,
    [Stamina]          INT    NOT NULL,
    [Keeper]           INT    NOT NULL,
    [Defending]        INT    NOT NULL,
    [Playmaking]       INT    NOT NULL,
    [Winger]           INT    NOT NULL,
    [Passing]          INT    NOT NULL,
    [Scoring]          INT    NOT NULL,
    [SetPieces]        INT    NOT NULL,
    [Loyalty]          INT    NOT NULL,
    [Experience]       INT    NOT NULL,
    [PlayerHattrickId] BIGINT NOT NULL,
    CONSTRAINT [PK_PlayerSkillSet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlayerSkillSet_Player] FOREIGN KEY ([PlayerHattrickId]) REFERENCES [senior].[Player] ([HattrickId])
);






GO
CREATE NONCLUSTERED INDEX [IX_PlayerSkillSet_PlayerHattrickId]
    ON [senior].[PlayerSkillSet]([PlayerHattrickId] ASC);

