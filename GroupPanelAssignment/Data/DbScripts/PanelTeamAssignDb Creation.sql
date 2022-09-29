USE master
go
CREATE DATABASE GroPanDb
go
USE GroPanDb
go
/* 
 * TABLE: AppUser 
 */

CREATE TABLE AppUser(
    UserId        int              IDENTITY(1,1),
    SpecialId     nvarchar(100)    NULL,
    Firstname     nvarchar(100)    NOT NULL,
    Othernames    nvarchar(100)    NULL,
    Surname       nvarchar(100)    NOT NULL,
    Created       datetime         NOT NULL,
    CreatedBy     nvarchar(100)    NOT NULL,
    Updated       datetime         NULL,
    UpdatedBy     nvarchar(100)    NULL,
    CONSTRAINT PK_AppUser PRIMARY KEY NONCLUSTERED (UserId)
)
go



IF OBJECT_ID('AppUser') IS NOT NULL
    PRINT '<<< CREATED TABLE AppUser >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AppUser >>>'
go

/* 
 * TABLE: AppUserAssignmentSession 
 */

CREATE TABLE AppUserAssignmentSession(
    AppUserAssignmentSessionId    int              IDENTITY(1,1),
    UserId                        int              NOT NULL,
    AssignmentSessionId           int              NOT NULL,
    Created                       datetime         NOT NULL,
    CreatedBy                     nvarchar(100)    NOT NULL,
    Updated                       datetime         NULL,
    UpdatedBy                     nvarchar(100)    NULL,
    CONSTRAINT PK_AppUserAssignmentSession PRIMARY KEY NONCLUSTERED (AppUserAssignmentSessionId)
)
go



IF OBJECT_ID('AppUserAssignmentSession') IS NOT NULL
    PRINT '<<< CREATED TABLE AppUserAssignmentSession >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AppUserAssignmentSession >>>'
go

/* 
 * TABLE: AssignmentSession 
 */

CREATE TABLE AssignmentSession(
    AssignmentSessionId    int              IDENTITY(1,1),
    SessionName            nvarchar(100)    NOT NULL,
    IsCurrent              bit              NOT NULL,
    Created                datetime         NOT NULL,
    CreatedBy              nvarchar(100)    NOT NULL,
    Updated                datetime         NULL,
    UpdatedBy              nvarchar(100)    NULL,
    CONSTRAINT PK_AssignmentSession PRIMARY KEY NONCLUSTERED (AssignmentSessionId)
)
go



IF OBJECT_ID('AssignmentSession') IS NOT NULL
    PRINT '<<< CREATED TABLE AssignmentSession >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AssignmentSession >>>'
go

/* 
 * TABLE: CWAGrouping 
 */

CREATE TABLE CWAGrouping(
    CWAGroupingId          int               IDENTITY(1,1),
    AssignmentSessionId    int               NOT NULL,
    Min                    decimal(10, 2)    NOT NULL,
    Max                    decimal(10, 2)    NOT NULL,
    Created                datetime          NOT NULL,
    CreatedBy              nvarchar(100)     NOT NULL,
    Updated                datetime          NULL,
    UpdatedBy              nvarchar(100)     NULL,
    CONSTRAINT PK_CWAGrouping PRIMARY KEY NONCLUSTERED (CWAGroupingId)
)
go



IF OBJECT_ID('CWAGrouping') IS NOT NULL
    PRINT '<<< CREATED TABLE CWAGrouping >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE CWAGrouping >>>'
go

/* 
 * TABLE: Location 
 */

CREATE TABLE Location(
    LocationId      int              IDENTITY(1,1),
    LocationName    nvarchar(100)    NOT NULL,
    Created         datetime         NOT NULL,
    CreatedBy       nvarchar(100)    NOT NULL,
    Updated         datetime         NULL,
    UpdatedBy       nvarchar(100)    NULL,
    CONSTRAINT PK_Location PRIMARY KEY NONCLUSTERED (LocationId)
)
go



IF OBJECT_ID('Location') IS NOT NULL
    PRINT '<<< CREATED TABLE Location >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Location >>>'
go

/* 
 * TABLE: Panel 
 */

CREATE TABLE Panel(
    PanelId                int              IDENTITY(1,1),
    PanelName              nvarchar(100)    NOT NULL,
    AssignmentSessionId    int              NOT NULL,
    LocationId             int              NULL,
    Created                datetime         NOT NULL,
    CreatedBy              nvarchar(100)    NOT NULL,
    Updated                datetime         NULL,
    UpdatedBy              nvarchar(100)    NULL,
    CONSTRAINT PK_Panel PRIMARY KEY NONCLUSTERED (PanelId)
)
go



IF OBJECT_ID('Panel') IS NOT NULL
    PRINT '<<< CREATED TABLE Panel >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Panel >>>'
go

/* 
 * TABLE: PanelMember 
 */

CREATE TABLE PanelMember(
    PanelMemberId    int              IDENTITY(1,1),
    PanelId          int              NOT NULL,
    UserId           int              NOT NULL,
    Created          datetime         NOT NULL,
    CreatedBy        nvarchar(100)    NOT NULL,
    Updated          datetime         NULL,
    UpdatedBy        nvarchar(100)    NULL,
    CONSTRAINT PK_PanelMember PRIMARY KEY NONCLUSTERED (PanelMemberId)
)
go



IF OBJECT_ID('PanelMember') IS NOT NULL
    PRINT '<<< CREATED TABLE PanelMember >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PanelMember >>>'
go

/* 
 * TABLE: PanelMemberTeamMemberScore 
 */

CREATE TABLE PanelMemberTeamMemberScore(
    PanelMemberTeamMemberScoreId    int              IDENTITY(1,1),
    ScoringSessionId                int              NOT NULL,
    PanelMemberId                   int              NOT NULL,
    TeamMemberId                    int              NOT NULL,
    SessionScoreItemId              int              NOT NULL,
    Created                         datetime         NOT NULL,
    CreatedBy                       nvarchar(100)    NOT NULL,
    Updated                         datetime         NULL,
    UpdatedBy                       nvarchar(100)    NULL,
    CONSTRAINT PK_PanelMemberTeamMemberScore PRIMARY KEY NONCLUSTERED (PanelMemberTeamMemberScoreId)
)
go



IF OBJECT_ID('PanelMemberTeamMemberScore') IS NOT NULL
    PRINT '<<< CREATED TABLE PanelMemberTeamMemberScore >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PanelMemberTeamMemberScore >>>'
go

/* 
 * TABLE: PanelMemberTeamScore 
 */

CREATE TABLE PanelMemberTeamScore(
    PanelMemberTeamScoreId    int              IDENTITY(1,1),
    ScoringSessionId          int              NOT NULL,
    PanelMemberId             int              NOT NULL,
    TeamId                    int              NOT NULL,
    SessionScoreItemId        int              NOT NULL,
    Created                   datetime         NOT NULL,
    CreatedBy                 nvarchar(100)    NOT NULL,
    Updated                   datetime         NULL,
    UpdatedBy                 nvarchar(100)    NULL,
    CONSTRAINT PK_PanelMemberTeamScore PRIMARY KEY NONCLUSTERED (PanelMemberTeamScoreId)
)
go



IF OBJECT_ID('PanelMemberTeamScore') IS NOT NULL
    PRINT '<<< CREATED TABLE PanelMemberTeamScore >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PanelMemberTeamScore >>>'
go

/* 
 * TABLE: PanelTeam 
 */

CREATE TABLE PanelTeam(
    PanelTeamId    int              IDENTITY(1,1),
    PanelId        int              NOT NULL,
    TeamId         int              NOT NULL,
    Created        datetime         NOT NULL,
    CreatedBy      nvarchar(100)    NOT NULL,
    Updated        datetime         NULL,
    UpdatedBy      nvarchar(100)    NULL,
    CONSTRAINT PK_PanelTeam PRIMARY KEY NONCLUSTERED (PanelTeamId)
)
go



IF OBJECT_ID('PanelTeam') IS NOT NULL
    PRINT '<<< CREATED TABLE PanelTeam >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE PanelTeam >>>'
go

/* 
 * TABLE: Role 
 */

CREATE TABLE Role(
    RoleId       int              IDENTITY(1,1),
    RoleName     nvarchar(100)    NOT NULL,
    Created      datetime         NOT NULL,
    CreatedBy    nvarchar(100)    NOT NULL,
    Updated      datetime         NULL,
    UpdatedBy    nvarchar(100)    NULL,
    CONSTRAINT PK_Role PRIMARY KEY NONCLUSTERED (RoleId)
)
go



IF OBJECT_ID('Role') IS NOT NULL
    PRINT '<<< CREATED TABLE Role >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Role >>>'
go

/* 
 * TABLE: ScoreItem 
 */

CREATE TABLE ScoreItem(
    ScoreItemId        int               IDENTITY(1,1),
    ScoreItemName      nvarchar(150)     NOT NULL,
    MinimumScore       decimal(18, 2)    NOT NULL,
    MaximumScore       decimal(18, 2)    NOT NULL,
    ScoreItemTypeId    int               NOT NULL,
    IsMandatory        bit               NOT NULL,
    IsActive           bit               NOT NULL,
    Created            datetime          NOT NULL,
    CreatedBy          nvarchar(100)     NOT NULL,
    Updated            datetime          NULL,
    UpdatedBy          nvarchar(100)     NULL,
    CONSTRAINT PK_ScoreItem PRIMARY KEY NONCLUSTERED (ScoreItemId)
)
go



IF OBJECT_ID('ScoreItem') IS NOT NULL
    PRINT '<<< CREATED TABLE ScoreItem >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE ScoreItem >>>'
go

/* 
 * TABLE: ScoreItemType 
 */

CREATE TABLE ScoreItemType(
    ScoreItemTypeId      int              IDENTITY(1,1),
    ScoreItemTypeName    nvarchar(100)    NOT NULL,
    Created              datetime         NOT NULL,
    CreatedBy            nvarchar(100)    NOT NULL,
    Updated              datetime         NULL,
    UpdatedBy            nvarchar(100)    NULL,
    CONSTRAINT PK_ScoreItemType PRIMARY KEY NONCLUSTERED (ScoreItemTypeId)
)
go



IF OBJECT_ID('ScoreItemType') IS NOT NULL
    PRINT '<<< CREATED TABLE ScoreItemType >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE ScoreItemType >>>'
go

/* 
 * TABLE: ScoringSession 
 */

CREATE TABLE ScoringSession(
    ScoringSessionId       int              IDENTITY(1,1),
    AssignmentSessionId    int              NOT NULL,
    ScoringSessionName     nvarchar(100)    NOT NULL,
    StartDate              datetime         NOT NULL,
    EndDate                datetime         NOT NULL,
    Created                datetime         NOT NULL,
    CreatedBy              nvarchar(100)    NOT NULL,
    Updated                datetime         NULL,
    UpdatedBy              nvarchar(100)    NULL,
    CONSTRAINT PK_ScoringSession PRIMARY KEY NONCLUSTERED (ScoringSessionId)
)
go



IF OBJECT_ID('ScoringSession') IS NOT NULL
    PRINT '<<< CREATED TABLE ScoringSession >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE ScoringSession >>>'
go

/* 
 * TABLE: SessionScoreItem 
 */

CREATE TABLE SessionScoreItem(
    SessionScoreItemId     int              IDENTITY(1,1),
    AssignmentSessionId    int              NOT NULL,
    ScoreItemId            int              NOT NULL,
    IsDeleted              bit              NOT NULL,
    Created                datetime         NOT NULL,
    CreatedBy              nvarchar(100)    NOT NULL,
    Updated                datetime         NULL,
    UpdatedBy              nvarchar(100)    NULL,
    CONSTRAINT PK_SessionScoreItem PRIMARY KEY NONCLUSTERED (SessionScoreItemId)
)
go



IF OBJECT_ID('SessionScoreItem') IS NOT NULL
    PRINT '<<< CREATED TABLE SessionScoreItem >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE SessionScoreItem >>>'
go

/* 
 * TABLE: StudentCWA 
 */

CREATE TABLE StudentCWA(
    StudentCWAId           int               IDENTITY(1,1),
    UserId                 int               NOT NULL,
    CWA                    decimal(18, 2)    NOT NULL,
    AssignmentSessionId    int               NOT NULL,
    Created                datetime          NOT NULL,
    CreatedBy              nvarchar(100)     NOT NULL,
    Updated                datetime          NULL,
    UpdatedBy              nvarchar(100)     NULL,
    CONSTRAINT PK_StudentCWA PRIMARY KEY NONCLUSTERED (StudentCWAId)
)
go



IF OBJECT_ID('StudentCWA') IS NOT NULL
    PRINT '<<< CREATED TABLE StudentCWA >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE StudentCWA >>>'
go

/* 
 * TABLE: Team 
 */

CREATE TABLE Team(
    TeamId                 int              IDENTITY(1,1),
    TeamName               nvarchar(150)    NOT NULL,
    Topic                  nvarchar(100)    NULL,
    AssignmentSessionId    int              NOT NULL,
    Created                datetime         NOT NULL,
    CreatedBy              nvarchar(100)    NOT NULL,
    Updated                datetime         NULL,
    UpdatedBy              nvarchar(100)    NULL,
    CONSTRAINT PK_Team PRIMARY KEY NONCLUSTERED (TeamId)
)
go



IF OBJECT_ID('Team') IS NOT NULL
    PRINT '<<< CREATED TABLE Team >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Team >>>'
go

/* 
 * TABLE: TeamMember 
 */

CREATE TABLE TeamMember(
    TeamMemberId    int              IDENTITY(1,1),
    TeamId          int              NOT NULL,
    UserId          int              NOT NULL,
    Created         datetime         NOT NULL,
    CreatedBy       nvarchar(100)    NOT NULL,
    Updated         datetime         NULL,
    UpdatedBy       nvarchar(10)     NULL,
    CONSTRAINT PK_TeamMember PRIMARY KEY NONCLUSTERED (TeamMemberId)
)
go



IF OBJECT_ID('TeamMember') IS NOT NULL
    PRINT '<<< CREATED TABLE TeamMember >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE TeamMember >>>'
go

/* 
 * TABLE: TeamSplitHistory 
 */

CREATE TABLE TeamSplitHistory(
    TeamSplitHistoryId    int              IDENTITY(1,1),
    TeamId                int              NOT NULL,
    ParentTeamId          int              NOT NULL,
    Created               datetime         NOT NULL,
    CreatedBy             nvarchar(100)    NOT NULL,
    Updated               datetime         NULL,
    UpdatedBy             nvarchar(100)    NULL,
    CONSTRAINT PK_TeamSplitHistory PRIMARY KEY NONCLUSTERED (TeamSplitHistoryId)
)
go



IF OBJECT_ID('TeamSplitHistory') IS NOT NULL
    PRINT '<<< CREATED TABLE TeamSplitHistory >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE TeamSplitHistory >>>'
go

/* 
 * TABLE: TeamSupervisor 
 */

CREATE TABLE TeamSupervisor(
    TeamSupervisorId    int              IDENTITY(1,1),
    TeamId              int              NOT NULL,
    UserId              int              NOT NULL,
    Created             datetime         NOT NULL,
    CreatedBy           nvarchar(100)    NOT NULL,
    Updated             datetime         NULL,
    UpdatedBy           nvarchar(100)    NULL,
    CONSTRAINT PK_TeamSupervisor PRIMARY KEY NONCLUSTERED (TeamSupervisorId)
)
go



IF OBJECT_ID('TeamSupervisor') IS NOT NULL
    PRINT '<<< CREATED TABLE TeamSupervisor >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE TeamSupervisor >>>'
go

/* 
 * TABLE: UserRole 
 */

CREATE TABLE UserRole(
    UserRoleId    int              IDENTITY(1,1),
    UserId        int              NOT NULL,
    RoleId        int              NOT NULL,
    Created       datetime         NOT NULL,
    CreatedBy     nvarchar(100)    NOT NULL,
    Updated       datetime         NULL,
    UpdatedBy     nvarchar(100)    NULL,
    CONSTRAINT PK_UserRole PRIMARY KEY NONCLUSTERED (UserRoleId)
)
go



IF OBJECT_ID('UserRole') IS NOT NULL
    PRINT '<<< CREATED TABLE UserRole >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE UserRole >>>'
go

/* 
 * TABLE: AppUserAssignmentSession 
 */

ALTER TABLE AppUserAssignmentSession ADD CONSTRAINT FK_AppUserAssignmentSession_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE AppUserAssignmentSession ADD CONSTRAINT FK_AppUserAssignmentSession_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go


/* 
 * TABLE: CWAGrouping 
 */

ALTER TABLE CWAGrouping ADD CONSTRAINT FK_CWAGrouping_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go


/* 
 * TABLE: Panel 
 */

ALTER TABLE Panel ADD CONSTRAINT FK_Panel_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go

ALTER TABLE Panel ADD CONSTRAINT FK_Panel_Location 
    FOREIGN KEY (LocationId)
    REFERENCES Location(LocationId)
go


/* 
 * TABLE: PanelMember 
 */

ALTER TABLE PanelMember ADD CONSTRAINT FK_PanelMember_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE PanelMember ADD CONSTRAINT FK_PanelMember_Panel 
    FOREIGN KEY (PanelId)
    REFERENCES Panel(PanelId)
go


/* 
 * TABLE: PanelMemberTeamMemberScore 
 */

ALTER TABLE PanelMemberTeamMemberScore ADD CONSTRAINT FK_PanelMemberTeamMemberScore_PanelMember 
    FOREIGN KEY (PanelMemberId)
    REFERENCES PanelMember(PanelMemberId)
go

ALTER TABLE PanelMemberTeamMemberScore ADD CONSTRAINT FK_PanelMemberTeamMemberScore_ScoringSession 
    FOREIGN KEY (ScoringSessionId)
    REFERENCES ScoringSession(ScoringSessionId)
go

ALTER TABLE PanelMemberTeamMemberScore ADD CONSTRAINT FK_PanelMemberTeamMemberScore_SessionScoreItem 
    FOREIGN KEY (SessionScoreItemId)
    REFERENCES SessionScoreItem(SessionScoreItemId)
go

ALTER TABLE PanelMemberTeamMemberScore ADD CONSTRAINT FK_PanelMemberTeamMemberScore_TeamMember 
    FOREIGN KEY (TeamMemberId)
    REFERENCES TeamMember(TeamMemberId)
go


/* 
 * TABLE: PanelMemberTeamScore 
 */

ALTER TABLE PanelMemberTeamScore ADD CONSTRAINT FK_PanelMemberTeamScore_PanelMember 
    FOREIGN KEY (PanelMemberId)
    REFERENCES PanelMember(PanelMemberId)
go

ALTER TABLE PanelMemberTeamScore ADD CONSTRAINT FK_PanelMemberTeamScore_ScoringSession 
    FOREIGN KEY (ScoringSessionId)
    REFERENCES ScoringSession(ScoringSessionId)
go

ALTER TABLE PanelMemberTeamScore ADD CONSTRAINT FK_PanelMemberTeamScore_SessionScoreItem 
    FOREIGN KEY (SessionScoreItemId)
    REFERENCES SessionScoreItem(SessionScoreItemId)
go

ALTER TABLE PanelMemberTeamScore ADD CONSTRAINT FK_PanelMemberTeamScore_Team 
    FOREIGN KEY (TeamId)
    REFERENCES Team(TeamId)
go


/* 
 * TABLE: PanelTeam 
 */

ALTER TABLE PanelTeam ADD CONSTRAINT FK_PanelTeam_Panel 
    FOREIGN KEY (PanelId)
    REFERENCES Panel(PanelId)
go

ALTER TABLE PanelTeam ADD CONSTRAINT FK_PanelTeam_Team 
    FOREIGN KEY (TeamId)
    REFERENCES Team(TeamId)
go


/* 
 * TABLE: ScoreItem 
 */

ALTER TABLE ScoreItem ADD CONSTRAINT FK_ScoreItem_ScoreItemType 
    FOREIGN KEY (ScoreItemTypeId)
    REFERENCES ScoreItemType(ScoreItemTypeId)
go


/* 
 * TABLE: ScoringSession 
 */

ALTER TABLE ScoringSession ADD CONSTRAINT FK_ScoringSession_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go


/* 
 * TABLE: SessionScoreItem 
 */

ALTER TABLE SessionScoreItem ADD CONSTRAINT FK_SessionScoreItem_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go

ALTER TABLE SessionScoreItem ADD CONSTRAINT FK_SessionScoreItem_ScoreItem 
    FOREIGN KEY (ScoreItemId)
    REFERENCES ScoreItem(ScoreItemId)
go


/* 
 * TABLE: StudentCWA 
 */

ALTER TABLE StudentCWA ADD CONSTRAINT FK_StudentCWA_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE StudentCWA ADD CONSTRAINT FK_StudentCWA_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go


/* 
 * TABLE: Team 
 */

ALTER TABLE Team ADD CONSTRAINT FK_Team_AssignmentSession 
    FOREIGN KEY (AssignmentSessionId)
    REFERENCES AssignmentSession(AssignmentSessionId)
go


/* 
 * TABLE: TeamMember 
 */

ALTER TABLE TeamMember ADD CONSTRAINT FK_TeamMember_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE TeamMember ADD CONSTRAINT FK_TeamMember_Team 
    FOREIGN KEY (TeamId)
    REFERENCES Team(TeamId)
go


/* 
 * TABLE: TeamSplitHistory 
 */

ALTER TABLE TeamSplitHistory ADD CONSTRAINT FK_TeamSplitHistory_Te2 
    FOREIGN KEY (TeamId)
    REFERENCES Team(TeamId)
go

ALTER TABLE TeamSplitHistory ADD CONSTRAINT FK_TeamSplitHistory_Te4 
    FOREIGN KEY (ParentTeamId)
    REFERENCES Team(TeamId)
go


/* 
 * TABLE: TeamSupervisor 
 */

ALTER TABLE TeamSupervisor ADD CONSTRAINT FK_TeamSupervisor_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE TeamSupervisor ADD CONSTRAINT FK_TeamSupervisor_Team 
    FOREIGN KEY (TeamId)
    REFERENCES Team(TeamId)
go


/* 
 * TABLE: UserRole 
 */

ALTER TABLE UserRole ADD CONSTRAINT FK_UserRole_AppUser 
    FOREIGN KEY (UserId)
    REFERENCES AppUser(UserId)
go

ALTER TABLE UserRole ADD CONSTRAINT FK_UserRole_Role 
    FOREIGN KEY (RoleId)
    REFERENCES Role(RoleId)
go


