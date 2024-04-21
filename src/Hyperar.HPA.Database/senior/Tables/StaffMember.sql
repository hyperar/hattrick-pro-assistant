CREATE TABLE [senior].[StaffMember] (
    [HattrickId]                 BIGINT          NOT NULL,
    [Name]                       NVARCHAR (256)  NOT NULL,
    [HiredOn]                    DATETIME        NOT NULL,
    [Type]                       TINYINT         NOT NULL,
    [Level]                      TINYINT         NOT NULL,
    [Salary]                     BIGINT          NOT NULL,
    [AvatarBytes]                VARBINARY (MAX) NOT NULL,
    [HallOfFamePlayerHattrickId] BIGINT          NULL,
    [FK_StaffMember_Team]        BIGINT          NOT NULL,
    [TeamId]                     BIGINT          NOT NULL,
    CONSTRAINT [PK_StaffMember] PRIMARY KEY CLUSTERED ([HattrickId] ASC),
    CONSTRAINT [FK_StaffMember_HallOfFamePlayer] FOREIGN KEY ([HallOfFamePlayerHattrickId]) REFERENCES [senior].[HallOfFamePlayer] ([HattrickId]),
    CONSTRAINT [FK_StaffMember_Team] FOREIGN KEY ([FK_StaffMember_Team]) REFERENCES [senior].[Team] ([HattrickId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StaffMember_HallOfFamePlayerHattrickId]
    ON [senior].[StaffMember]([HallOfFamePlayerHattrickId] ASC) WHERE ([HallOfFamePlayerHattrickId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_StaffMember_FK_StaffMember_Team]
    ON [senior].[StaffMember]([FK_StaffMember_Team] ASC);

