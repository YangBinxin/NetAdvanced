using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Service.Mvc.Models
{
    public partial class DbBeginStudyContext : DbContext
    {
        public DbBeginStudyContext()
        {
        }

        public DbBeginStudyContext(DbContextOptions<DbBeginStudyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<StuInfo> StuInfo { get; set; }
        public virtual DbSet<TbUser> TbUser { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<ZouNi> ZouNi { get; set; }
        public virtual DbSet<AAAAA> AAAAA { get; set; }

        // Unable to generate entity type for table 'dbo.testData'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tradelog'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tradelog_partition1'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.ExamNo)
                    .HasName("PK_ExamNo");

                entity.Property(e => e.LadExam)
                    .HasColumnName("ladExam")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WriteExam)
                    .HasColumnName("writeExam")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<StuInfo>(entity =>
            {
                entity.HasKey(e => e.StuId);

                entity.Property(e => e.StuAge)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StuName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StuSex)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('男')");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK_U_ID");

                entity.ToTable("TB_USER");

                entity.HasIndex(e => e.ULoginName)
                    .HasName("UQ_LoginName")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("U_ID");

                entity.Property(e => e.DId)
                    .HasColumnName("D_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Flag).HasColumnName("FLAG");

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UAddress)
                    .HasColumnName("U_ADDRESS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UAnswer)
                    .HasColumnName("U_ANSWER")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.UBrithday)
                    .HasColumnName("U_BRITHDAY")
                    .HasColumnType("datetime");

                entity.Property(e => e.UBrithplace)
                    .HasColumnName("U_BRITHPLACE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UEmail)
                    .HasColumnName("U_EMAIL")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UIdcard)
                    .HasColumnName("U_IDCARD")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ULastip)
                    .HasColumnName("U_LASTIP")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.ULastlogintime)
                    .HasColumnName("U_LASTLOGINTIME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ULoginName)
                    .HasColumnName("U_LOGIN_NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ULookPwd)
                    .HasColumnName("U_LOOK_PWD")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UPwd)
                    .HasColumnName("U_PWD")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UQuestion)
                    .HasColumnName("U_QUESTION")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.USex)
                    .HasColumnName("U_SEX")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UState).HasColumnName("U_STATE");

                entity.Property(e => e.UTel)
                    .HasColumnName("U_TEL")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UTruename)
                    .HasColumnName("U_TRUENAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UType).HasColumnName("U_TYPE");

                entity.Property(e => e.UZip)
                    .HasColumnName("U_ZIP")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.A);

                entity.Property(e => e.B).HasMaxLength(10);
            });
        }
    }
}
