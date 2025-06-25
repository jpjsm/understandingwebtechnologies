using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbCrudCore.Models
{
    public partial class fcmchangemanagersqlContext : DbContext
    {
        public fcmchangemanagersqlContext()
        {
        }

        public fcmchangemanagersqlContext(DbContextOptions<fcmchangemanagersqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExceptionRequestApproverNotes> ExceptionRequestApproverNotes { get; set; }
        public virtual DbSet<ExceptionRequestFormerApproverIds> ExceptionRequestFormerApproverIds { get; set; }
        public virtual DbSet<ExceptionRequestServiceSubscriptionRegions> ExceptionRequestServiceSubscriptionRegions { get; set; }
        public virtual DbSet<ExceptionRequestStatusChange> ExceptionRequestStatusChange { get; set; }
        public virtual DbSet<ExceptionRequests> ExceptionRequests { get; set; }
        public virtual DbSet<Test1> Test1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:fcm-changemanagersql.database.windows.net,1433;Initial Catalog=fcm-changemanagersql;Persist Security Info=False;User ID=changemanageradmin;Password=Ch1ng2M1n1g@r1dm3n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=600;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExceptionRequestApproverNotes>(entity =>
            {
                entity.HasKey(e => new { e.ExceptionRequestId, e.ApproverId });

                entity.Property(e => e.Notes).IsRequired();

                entity.HasOne(d => d.ExceptionRequest)
                    .WithMany(p => p.ExceptionRequestApproverNotes)
                    .HasForeignKey(d => d.ExceptionRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExceptionRequestApproverNotes_ExceptionRequests");
            });

            modelBuilder.Entity<ExceptionRequestFormerApproverIds>(entity =>
            {
                entity.HasKey(e => new { e.ExceptionRequestId, e.ApproverId });

                entity.HasOne(d => d.ExceptionRequest)
                    .WithMany(p => p.ExceptionRequestFormerApproverIds)
                    .HasForeignKey(d => d.ExceptionRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExceptionRequestFormerApproverIds_ExceptionRequests");
            });

            modelBuilder.Entity<ExceptionRequestServiceSubscriptionRegions>(entity =>
            {
                entity.HasKey(e => new { e.ExceptionRequestId, e.ServiceId, e.SubscriptionId, e.Region });

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.HasOne(d => d.ExceptionRequest)
                    .WithMany(p => p.ExceptionRequestServiceSubscriptionRegions)
                    .HasForeignKey(d => d.ExceptionRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExceptionRequestServiceSubscriptionRegions_ExceptionRequests");
            });

            modelBuilder.Entity<ExceptionRequestStatusChange>(entity =>
            {
                entity.HasKey(e => new { e.ExceptionRequestId, e.Status, e.ChangeDate });

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.ExceptionRequest)
                    .WithMany(p => p.ExceptionRequestStatusChange)
                    .HasForeignKey(d => d.ExceptionRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExceptionRequestStatusChange_ExceptionRequests");
            });

            modelBuilder.Entity<ExceptionRequests>(entity =>
            {
                entity.HasKey(e => e.ExceptionRequestId);

                entity.Property(e => e.ExceptionRequestId).ValueGeneratedNever();

                entity.Property(e => e.ApproverEmail).HasMaxLength(256);

                entity.Property(e => e.ApproverName).HasMaxLength(128);

                entity.Property(e => e.ExceptionRequestStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestorEmail).HasMaxLength(256);

                entity.Property(e => e.RequestorName).HasMaxLength(128);

                entity.Property(e => e.Title).HasMaxLength(128);
            });

            modelBuilder.Entity<Test1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Test_1");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
