using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class database_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    MotDePasse = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ClesMotDePasse = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    DateNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LieuNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgenceId = table.Column<int>(type: "int", nullable: false),
                    DateAdhesion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membres_Agences_AgenceId",
                        column: x => x.AgenceId,
                        principalTable: "Agences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adhesions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    DateAdhesion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adhesions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adhesions_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    Duree = table.Column<int>(type: "int", nullable: false),
                    MontantCapital = table.Column<double>(type: "float", nullable: false),
                    DateDemande = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDecaissement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avances_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    DateCotisation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salaire = table.Column<double>(type: "float", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotisations_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    Duree = table.Column<int>(type: "int", nullable: false),
                    MontantCapital = table.Column<double>(type: "float", nullable: false),
                    MontantCommission = table.Column<double>(type: "float", nullable: false),
                    MontantInterets = table.Column<double>(type: "float", nullable: false),
                    DateDemande = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDecaissement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Echeances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEcheance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontantCapital = table.Column<double>(type: "float", nullable: false),
                    MontantCommission = table.Column<double>(type: "float", nullable: false),
                    MontantInterets = table.Column<double>(type: "float", nullable: false),
                    CreditId = table.Column<int>(type: "int", nullable: false),
                    AvanceId = table.Column<int>(type: "int", nullable: false),
                    DatePaiement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAnticipation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echeances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Echeances_Avances_AvanceId",
                        column: x => x.AvanceId,
                        principalTable: "Avances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Echeances_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Mouvements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateMouvement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontantDebit = table.Column<double>(type: "float", nullable: false),
                    MontantCredit = table.Column<double>(type: "float", nullable: false),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    CotisationId = table.Column<int>(type: "int", nullable: false),
                    AdhesionId = table.Column<int>(type: "int", nullable: false),
                    AvanceId = table.Column<int>(type: "int", nullable: false),
                    CreditId = table.Column<int>(type: "int", nullable: false),
                    EcheanceId = table.Column<int>(type: "int", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiePar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouvements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mouvements_Avances_AvanceId",
                        column: x => x.AvanceId,
                        principalTable: "Avances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Mouvements_Cotisations_CotisationId",
                        column: x => x.CotisationId,
                        principalTable: "Cotisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Mouvements_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Mouvements_Echeances_EcheanceId",
                        column: x => x.EcheanceId,
                        principalTable: "Echeances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Mouvements_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adhesions_MembreId",
                table: "Adhesions",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Avances_MembreId",
                table: "Avances",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotisations_MembreId",
                table: "Cotisations",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_MembreId",
                table: "Credits",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Echeances_AvanceId",
                table: "Echeances",
                column: "AvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Echeances_CreditId",
                table: "Echeances",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Membres_AgenceId",
                table: "Membres",
                column: "AgenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_AvanceId",
                table: "Mouvements",
                column: "AvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_CotisationId",
                table: "Mouvements",
                column: "CotisationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_CreditId",
                table: "Mouvements",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_EcheanceId",
                table: "Mouvements",
                column: "EcheanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_MembreId",
                table: "Mouvements",
                column: "MembreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adhesions");

            migrationBuilder.DropTable(
                name: "Mouvements");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Cotisations");

            migrationBuilder.DropTable(
                name: "Echeances");

            migrationBuilder.DropTable(
                name: "Avances");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Membres");

            migrationBuilder.DropTable(
                name: "Agences");
        }
    }
}
