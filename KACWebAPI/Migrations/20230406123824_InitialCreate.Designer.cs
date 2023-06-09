﻿// <auto-generated />
using System;
using KACCloudContextLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KACWebAPI.Migrations
{
    [DbContext(typeof(KACCloudContext))]
    [Migration("20230406123824_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KACCloudContextLibrary.Domain.Entity.Anagrafica", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uuid");

                    b.Property<string>("CodiceFiscale")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Anagrafiche", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
