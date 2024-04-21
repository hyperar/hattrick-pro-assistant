CREATE TABLE [senior].[MatchTeam] (
    [Id]                             INT             IDENTITY (1, 1) NOT NULL,
    [HattrickId]                     BIGINT          NOT NULL,
    [Name]                           NVARCHAR (256)  NOT NULL,
    [MatchKitUrl]                    NVARCHAR (1024) NULL,
    [MatchKitBytes]                  VARBINARY (MAX) NULL,
    [Formation]                      NVARCHAR (20)   NULL,
    [Score]                          TINYINT         NULL,
    [TacticType]                     TINYINT         NULL,
    [TacticLevel]                    TINYINT         NULL,
    [MidfieldRating]                 TINYINT         NULL,
    [RightDefenseRating]             TINYINT         NULL,
    [CentralDefenseRating]           TINYINT         NULL,
    [LeftDefenseRating]              TINYINT         NULL,
    [RightAttackRating]              TINYINT         NULL,
    [CentralAttackRating]            TINYINT         NULL,
    [LeftAttackRating]               TINYINT         NULL,
    [DefenseIndirectSetPiecesRating] TINYINT         NULL,
    [AttackIndirectSetPiecesRating]  TINYINT         NULL,
    [Attitude]                       SMALLINT        NULL,
    [ChancesOnRight]                 TINYINT         NULL,
    [ChancesOnCenter]                TINYINT         NULL,
    [ChancesOnLeft]                  TINYINT         NULL,
    [SpecialEventChances]            TINYINT         NULL,
    [OtherChances]                   TINYINT         NULL,
    [FirstHalfPosession]             TINYINT         NULL,
    [SecondHalfPosession]            TINYINT         NULL,
    [MatchHattrickId]                BIGINT          NOT NULL,
    CONSTRAINT [PK_MatchTeam] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeam_Match] FOREIGN KEY ([MatchHattrickId]) REFERENCES [senior].[Match] ([HattrickId])
);




GO
CREATE NONCLUSTERED INDEX [IX_MatchTeam_MatchHattrickId]
    ON [senior].[MatchTeam]([MatchHattrickId] ASC);

