using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class PanelTeamAssignDbContext : DbContext
    {
        public PanelTeamAssignDbContext()
        {
        }

        public PanelTeamAssignDbContext(DbContextOptions<PanelTeamAssignDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AssignmentSession> AssignmentSessions { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Panel> Panels { get; set; }
        public virtual DbSet<PanelMember> PanelMembers { get; set; }
        public virtual DbSet<PanelMemberTeamMemberScore> PanelMemberTeamMemberScores { get; set; }
        public virtual DbSet<PanelMemberTeamScore> PanelMemberTeamScores { get; set; }
        public virtual DbSet<PanelTeam> PanelTeams { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScoreItem> ScoreItems { get; set; }
        public virtual DbSet<ScoreItemType> ScoreItemTypes { get; set; }
        public virtual DbSet<ScoringSession> ScoringSessions { get; set; }
        public virtual DbSet<SessionScoreItem> SessionScoreItems { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<TeamSplitHistory> TeamSplitHistories { get; set; }
        public virtual DbSet<TeamSupervisor> TeamSupervisors { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .IsClustered(false);

                entity.ToTable("AppUser");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Othernames).HasMaxLength(100);

                entity.Property(e => e.SpecialId).HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<AssignmentSession>(entity =>
            {
                entity.HasKey(e => e.AssignmentSessionId)
                    .IsClustered(false);

                entity.ToTable("AssignmentSession");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SessionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Panel>(entity =>
            {
                entity.HasKey(e => e.PanelId)
                    .IsClustered(false);

                entity.ToTable("Panel");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PanelName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.AssignmentSession)
                    .WithMany(p => p.Panels)
                    .HasForeignKey(d => d.AssignmentSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Panel_AssignmentSession");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Panels)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Panel_Location");
            });

            modelBuilder.Entity<PanelMember>(entity =>
            {
                entity.HasKey(e => e.PanelMemberId)
                    .IsClustered(false);

                entity.ToTable("PanelMember");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.Panel)
                    .WithMany(p => p.PanelMembers)
                    .HasForeignKey(d => d.PanelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMember_Panel");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PanelMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMember_AppUser");
            });

            modelBuilder.Entity<PanelMemberTeamMemberScore>(entity =>
            {
                entity.HasKey(e => e.PanelMemberTeamMemberScoreId)
                    .IsClustered(false);

                entity.ToTable("PanelMemberTeamMemberScore");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.PanelMember)
                    .WithMany(p => p.PanelMemberTeamMemberScores)
                    .HasForeignKey(d => d.PanelMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamMemberScore_PanelMember");

                entity.HasOne(d => d.ScoringSession)
                    .WithMany(p => p.PanelMemberTeamMemberScores)
                    .HasForeignKey(d => d.ScoringSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamMemberScore_ScoringSession");

                entity.HasOne(d => d.SessionScoreItem)
                    .WithMany(p => p.PanelMemberTeamMemberScores)
                    .HasForeignKey(d => d.SessionScoreItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamMemberScore_SessionScoreItem");

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.PanelMemberTeamMemberScores)
                    .HasForeignKey(d => d.TeamMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamMemberScore_TeamMember");
            });

            modelBuilder.Entity<PanelMemberTeamScore>(entity =>
            {
                entity.HasKey(e => e.PanelMemberTeamScoreId)
                    .IsClustered(false);

                entity.ToTable("PanelMemberTeamScore");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.PanelMember)
                    .WithMany(p => p.PanelMemberTeamScores)
                    .HasForeignKey(d => d.PanelMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamScore_PanelMember");

                entity.HasOne(d => d.ScoringSession)
                    .WithMany(p => p.PanelMemberTeamScores)
                    .HasForeignKey(d => d.ScoringSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamScore_ScoringSession");

                entity.HasOne(d => d.SessionScoreItem)
                    .WithMany(p => p.PanelMemberTeamScores)
                    .HasForeignKey(d => d.SessionScoreItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamScore_SessionScoreItem");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PanelMemberTeamScores)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelMemberTeamScore_Team");
            });

            modelBuilder.Entity<PanelTeam>(entity =>
            {
                entity.HasKey(e => e.PanelTeamId)
                    .IsClustered(false);

                entity.ToTable("PanelTeam");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.Panel)
                    .WithMany(p => p.PanelTeams)
                    .HasForeignKey(d => d.PanelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelTeam_Panel");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PanelTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanelTeam_Team");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .IsClustered(false);

                entity.ToTable("Role");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<ScoreItem>(entity =>
            {
                entity.HasKey(e => e.ScoreItemId)
                    .IsClustered(false);

                entity.ToTable("ScoreItem");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MaximumScore).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumScore).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ScoreItemName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.ScoreItemType)
                    .WithMany(p => p.ScoreItems)
                    .HasForeignKey(d => d.ScoreItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScoreItem_ScoreItemType");
            });

            modelBuilder.Entity<ScoreItemType>(entity =>
            {
                entity.HasKey(e => e.ScoreItemTypeId)
                    .IsClustered(false);

                entity.ToTable("ScoreItemType");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ScoreItemTypeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<ScoringSession>(entity =>
            {
                entity.HasKey(e => e.ScoringSessionId)
                    .IsClustered(false);

                entity.ToTable("ScoringSession");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ScoringSessionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.AssignmentSession)
                    .WithMany(p => p.ScoringSessions)
                    .HasForeignKey(d => d.AssignmentSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScoringSession_AssignmentSession");
            });

            modelBuilder.Entity<SessionScoreItem>(entity =>
            {
                entity.HasKey(e => e.SessionScoreItemId)
                    .IsClustered(false);

                entity.ToTable("SessionScoreItem");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.AssignmentSession)
                    .WithMany(p => p.SessionScoreItems)
                    .HasForeignKey(d => d.AssignmentSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionScoreItem_AssignmentSession");

                entity.HasOne(d => d.ScoreItem)
                    .WithMany(p => p.SessionScoreItems)
                    .HasForeignKey(d => d.ScoreItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionScoreItem_ScoreItem");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .IsClustered(false);

                entity.ToTable("Team");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.AssignmentSession)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.AssignmentSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team_AssignmentSession");
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.TeamMemberId)
                    .IsClustered(false);

                entity.ToTable("TeamMember");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(10);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMember_Team");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMember_AppUser");
            });

            modelBuilder.Entity<TeamSplitHistory>(entity =>
            {
                entity.HasKey(e => e.TeamSplitHistoryId)
                    .IsClustered(false);

                entity.ToTable("TeamSplitHistory");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.ParentTeam)
                    .WithMany(p => p.TeamSplitHistoryParentTeams)
                    .HasForeignKey(d => d.ParentTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSplitHistory_Te4");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamSplitHistoryTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSplitHistory_Team");
            });

            modelBuilder.Entity<TeamSupervisor>(entity =>
            {
                entity.HasKey(e => e.TeamSupervisorId)
                    .IsClustered(false);

                entity.ToTable("TeamSupervisor");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamSupervisors)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSupervisor_Team");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamSupervisors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamSupervisor_AppUser");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .IsClustered(false);

                entity.ToTable("UserRole");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_AppUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
