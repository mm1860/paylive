﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using paylive.Console.DbContext;

namespace paylive.Console.Migrations
{
    [DbContext(typeof(LiveContext))]
    partial class LiveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("paylive.Console.DbContext.Receivers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("ImNo");

                    b.Property<string>("Mobile");

                    b.HasKey("Id");

                    b.ToTable("Receivers");
                });

            modelBuilder.Entity("paylive.Console.DbContext.SmsQu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<bool>("Completed");

                    b.Property<string>("Msg");

                    b.Property<string>("Receivers");

                    b.HasKey("Id");

                    b.ToTable("SmsQu");
                });

            modelBuilder.Entity("paylive.Console.DbContext.WebImConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ssid");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("WebImConfig");
                });
        }
    }
}
