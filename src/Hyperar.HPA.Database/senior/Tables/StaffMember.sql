CREATE TABLE [senior].[StaffMember] (
    [HattrickId]                 BIGINT          NOT NULL,
    [Name]                       NVARCHAR (256)  NOT NULL,
    [HiredOn]                    DATETIME        NOT NULL,
    [Type]                       TINYINT         NOT NULL,
    [Level]                      TINYINT         NOT NULL,
    [Salary]                     BIGINT          NOT NULL,
    [AvatarBytes]                VARBINARY (MAX) NOT NULL,
    [HallOfFamePlayerHattrickId] BIGINT          NULL,
    [TeamHattrickId]             BIGINT          NOT NULL,
    CONSTRAINT [PK_StaffMember] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_StaffMember_HallOfFamePlayer] FOREIGN KEY ([HallOfFamePlayerHattrickId]) REFERENCES [senior].[HallOfFamePlayer] ([HattrickId]),
    CONSTRAINT [FK_StaffMember_Team] FOREIGN KEY ([TeamHattrickId]) REFERENCES [senior].[Team] ([HattrickId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StaffMember_HallOfFamePlayerHattrickId]
    ON [senior].[StaffMember]([HallOfFamePlayerHattrickId] ASC) WHERE ([HallOfFamePlayerHattrickId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_StaffMember_TeamHattrickId]
    ON [senior].[StaffMember]([TeamHattrickId] ASC);
