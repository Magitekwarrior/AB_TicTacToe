﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicTacToeAPI.Infrastructure.Repositories.DBContext;

namespace TicTacToeAPI.Infrastructure.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("TicTacToeAPI.Infrastructure.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell3")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell4")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell5")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell6")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell7")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell8")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cell9")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GameStartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Player1Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Player2Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Winner")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tictactoegame");
                });
#pragma warning restore 612, 618
        }
    }
}
