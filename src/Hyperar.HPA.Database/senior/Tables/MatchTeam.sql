CREATE TABLE [senior].[MatchTeam] (
    [Id]                             INT             IDENTITY (1, 1) NOT NULL,
    [HattrickId]                     BIGINT          NOT NULL,
    [Name]                           NVARCHAR (256)  NOT NULL,
    [Location]                       INT             NOT NULL,
    [MatchKitUrl]                    NVARCHAR (1024) NULL,
    [MatchKitBytes]                  VARBINARY (MAX) NULL,
    [Formation]                      NVARCHAR (20)   NULL,
    [Score]                          INT             NULL,
    [TacticType]                     INT             NULL,
    [TacticLevel]                    INT             NULL,
    [MidfieldRating]                 INT             NULL,
    [RightDefenseRating]             INT             NULL,
    [CentralDefenseRating]           INT             NULL,
    [LeftDefenseRating]              INT             NULL,
    [RightAttackRating]              INT             NULL,
    [CentralAttackRating]            INT             NULL,
    [LeftAttackRating]               INT             NULL,
    [DefenseIndirectSetPiecesRating] INT             NULL,
    [AttackIndirectSetPiecesRating]  INT             NULL,
    [Attitude]                       INT             NULL,
    [ChancesOnRight]                 INT             NULL,
    [ChancesOnCenter]                INT             NULL,
    [ChancesOnLeft]                  INT             NULL,
    [SpecialEventChances]            INT             NULL,
    [OtherChances]                   INT             NULL,
    [FirstHalfPosession]             INT             NULL,
    [SecondHalfPosession]            INT             NULL,
    [MatchHattrickId]                BIGINT          NOT NULL,
    CONSTRAINT [PK_MatchTeam] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Senior_MatchTeam_Match] FOREIGN KEY ([MatchHattrickId]) REFERENCES [senior].[Match] ([HattrickId])
);






GO
CREATE NONCLUSTERED INDEX [IX_MatchTeam_MatchHattrickId]
    ON [senior].[MatchTeam]([MatchHattrickId] ASC);

