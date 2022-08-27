﻿// <auto-generated />
using GestioneSagre.Domain.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestioneSagre.Web.Server.Migrations
{
    [DbContext(typeof(GestioneSagreDbContext))]
    [Migration("20220827134526_ResetMigration")]
    partial class ResetMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("GestioneSagre.Core.Models.Entities.CategoriaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoriaStampa")
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoriaVideo")
                        .HasColumnType("TEXT");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categoria", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Core.Models.Entities.FestaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataFine")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataInizio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Edizione")
                        .HasColumnType("TEXT");

                    b.Property<bool>("GestioneCoperti")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GestioneMenu")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Logo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Luogo")
                        .HasColumnType("TEXT");

                    b.Property<bool>("StampaCarta")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("StampaLogo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("StampaRicevuta")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StatusFesta")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Titolo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Festa", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Core.Models.Entities.ProdottoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AvvisoScorta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Prenotazione")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prodotto")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ProdottoAttivo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantita")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("QuantitaAttiva")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantitaScorta")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Prodotto", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Core.Models.Entities.ProdottoEntity", b =>
                {
                    b.OwnsOne("GestioneSagre.Core.Models.ValueObjects.Money", "Prezzo", b1 =>
                        {
                            b1.Property<int>("ProdottoEntityId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("ProdottoEntityId");

                            b1.ToTable("Prodotto");

                            b1.WithOwner()
                                .HasForeignKey("ProdottoEntityId");
                        });

                    b.Navigation("Prezzo");
                });
#pragma warning restore 612, 618
        }
    }
}
