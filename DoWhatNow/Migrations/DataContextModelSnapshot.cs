﻿// <auto-generated />
using System;
using DoWhatNow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoWhatNow.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DoWhatNow.Models.ToDoItem", b =>
                {
                    b.Property<int>("ToDoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToDoItemId"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ToDoListId")
                        .HasColumnType("int");

                    b.Property<string>("ToDoTask")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ToDoItemId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("DoWhatNow.Models.ToDoList", b =>
                {
                    b.Property<int>("ToDoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToDoListId"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ToDoListId");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("DoWhatNow.Models.ToDoItem", b =>
                {
                    b.HasOne("DoWhatNow.Models.ToDoList", null)
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoWhatNow.Models.ToDoList", b =>
                {
                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
