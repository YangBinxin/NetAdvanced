﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Mvc.Models;

namespace Service.Mvc.Migrations
{
    [DbContext(typeof(DbBeginStudyContext))]
    partial class DbBeginStudyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.Mvc.Models.AAAAA", b =>
                {
                    b.Property<int>("A")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("A");

                    b.ToTable("AAAAA");
                });

            modelBuilder.Entity("Service.Mvc.Models.Exam", b =>
                {
                    b.Property<int>("ExamNo")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("LadExam")
                        .HasColumnName("ladExam")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("StuId");

                    b.Property<decimal?>("WriteExam")
                        .HasColumnName("writeExam")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("ExamNo")
                        .HasName("PK_ExamNo");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Service.Mvc.Models.StuInfo", b =>
                {
                    b.Property<int>("StuId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StuAge")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("StuName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("StuSex")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('男')")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.HasKey("StuId");

                    b.ToTable("StuInfo");
                });

            modelBuilder.Entity("Service.Mvc.Models.TbUser", b =>
                {
                    b.Property<int>("UId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("U_ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DId")
                        .HasColumnName("D_ID")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Flag")
                        .HasColumnName("FLAG");

                    b.Property<string>("Remark")
                        .HasColumnName("REMARK")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("UAddress")
                        .HasColumnName("U_ADDRESS")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("UAnswer")
                        .HasColumnName("U_ANSWER")
                        .HasMaxLength(400)
                        .IsUnicode(false);

                    b.Property<DateTime?>("UBrithday")
                        .HasColumnName("U_BRITHDAY")
                        .HasColumnType("datetime");

                    b.Property<string>("UBrithplace")
                        .HasColumnName("U_BRITHPLACE")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("UEmail")
                        .HasColumnName("U_EMAIL")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("UIdcard")
                        .HasColumnName("U_IDCARD")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("ULastip")
                        .HasColumnName("U_LASTIP")
                        .HasMaxLength(35)
                        .IsUnicode(false);

                    b.Property<string>("ULastlogintime")
                        .HasColumnName("U_LASTLOGINTIME")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("ULoginName")
                        .HasColumnName("U_LOGIN_NAME")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("ULookPwd")
                        .HasColumnName("U_LOOK_PWD")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("UPwd")
                        .HasColumnName("U_PWD")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("UQuestion")
                        .HasColumnName("U_QUESTION")
                        .HasMaxLength(400)
                        .IsUnicode(false);

                    b.Property<string>("USex")
                        .HasColumnName("U_SEX")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("UState")
                        .HasColumnName("U_STATE");

                    b.Property<string>("UTel")
                        .HasColumnName("U_TEL")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("UTruename")
                        .HasColumnName("U_TRUENAME")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("UType")
                        .HasColumnName("U_TYPE");

                    b.Property<string>("UZip")
                        .HasColumnName("U_ZIP")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("UId")
                        .HasName("PK_U_ID");

                    b.HasIndex("ULoginName")
                        .IsUnique()
                        .HasName("UQ_LoginName")
                        .HasFilter("[U_LOGIN_NAME] IS NOT NULL");

                    b.ToTable("TB_USER");
                });

            modelBuilder.Entity("Service.Mvc.Models.Test", b =>
                {
                    b.Property<int>("A")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("B")
                        .HasMaxLength(10);

                    b.Property<string>("C");

                    b.Property<int>("D");

                    b.Property<int>("E");

                    b.HasKey("A");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Service.Mvc.Models.ZouNi", b =>
                {
                    b.Property<int>("A")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("B");

                    b.HasKey("A");

                    b.ToTable("ZouNi");
                });
#pragma warning restore 612, 618
        }
    }
}
