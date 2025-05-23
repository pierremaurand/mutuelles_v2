﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mutuelleApi.data;

#nullable disable

namespace mutuelleApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250511082602_UpdateAgence")]
    partial class UpdateAgence
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("mutuelleApi.models.Agence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agences");
                });

            modelBuilder.Entity("mutuelleApi.models.Avance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateEnregistrement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duree")
                        .HasColumnType("int");

                    b.Property<int>("MembreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCapital")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Avances");
                });

            modelBuilder.Entity("mutuelleApi.models.Compte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("mutuelleApi.models.Cotisation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateCotisation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("Montant")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Cotisations");
                });

            modelBuilder.Entity("mutuelleApi.models.Credit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateEnregistrement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duree")
                        .HasColumnType("int");

                    b.Property<int>("MembreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCapital")
                        .HasColumnType("real");

                    b.Property<float>("MontantInterets")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("mutuelleApi.models.Echeance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AvanceId")
                        .HasColumnType("int");

                    b.Property<int?>("CreditId")
                        .HasColumnType("int");

                    b.Property<string>("DateEcheance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCapital")
                        .HasColumnType("real");

                    b.Property<float>("MontantInterets")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Echeances");
                });

            modelBuilder.Entity("mutuelleApi.models.Ecriture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateEnregistrement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<int>("MouvementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ecritures");
                });

            modelBuilder.Entity("mutuelleApi.models.Gabarit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("EstActif")
                        .HasColumnType("bit");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Gabarits");
                });

            modelBuilder.Entity("mutuelleApi.models.Membre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgenceId")
                        .HasColumnType("int");

                    b.Property<string>("DateAdhesion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateNaissance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("EstActif")
                        .HasColumnType("bit");

                    b.Property<string>("LieuNaissance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sexe")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Membres");
                });

            modelBuilder.Entity("mutuelleApi.models.Mouvement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AvanceId")
                        .HasColumnType("int");

                    b.Property<int?>("CotisationId")
                        .HasColumnType("int");

                    b.Property<int?>("CreditId")
                        .HasColumnType("int");

                    b.Property<string>("DateMouvement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EcheanceId")
                        .HasColumnType("int");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MembreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCredit")
                        .HasColumnType("real");

                    b.Property<float>("MontantDebit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Mouvements");
                });

            modelBuilder.Entity("mutuelleApi.models.MouvementComptable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<int>("EcritureId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCredit")
                        .HasColumnType("real");

                    b.Property<float>("MontantDebit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("MouvementComptables");
                });

            modelBuilder.Entity("mutuelleApi.models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<int>("GabaritId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<float>("MontantCredit")
                        .HasColumnType("real");

                    b.Property<float>("MontantDebit")
                        .HasColumnType("real");

                    b.Property<bool>("MontantFixe")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("mutuelleApi.models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("ClesMotDePasse")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool?>("EstActif")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifieLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiePar")
                        .HasColumnType("int");

                    b.Property<byte[]>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });
#pragma warning restore 612, 618
        }
    }
}
