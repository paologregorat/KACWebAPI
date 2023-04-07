﻿// <auto-generated />
using System;
using KACGatewayContextLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EdgeGateway.Migrations
{
    [DbContext(typeof(KACGatewayContext))]
    [Migration("20230404093711_InitialCreate")]
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

            modelBuilder.Entity("KACGatewayContextLibrary.Domain.Entity.Anagrafica", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uuid");

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