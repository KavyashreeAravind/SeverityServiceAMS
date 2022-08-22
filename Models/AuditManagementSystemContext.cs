using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AuditSeverityService.Models
{
    public partial class AuditManagementSystemContext : DbContext
    {
        public AuditManagementSystemContext()
        {
        }

        public AuditManagementSystemContext(DbContextOptions<AuditManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Userdetails> Userdetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:kdh.database.windows.net,1433;Initial Catalog=AMS;Persist Security Info=False;User ID=kavya;Password=R@punzel050;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.Auditid).HasColumnName("auditid");

                entity.Property(e => e.ApplicationOwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.AuditType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectExecutionStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectManagerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("Userid");

                entity.HasOne(d => d.Userdetail)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__Audit__userid__47DBAE45");

                entity.Property(e => e.RemedialActionDuration).IsUnicode(false);
            });

            modelBuilder.Entity<Userdetails>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__Userdeta__CBA1B25773900E22");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
