using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class update_database_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mouvements_AvanceId",
                table: "Mouvements");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_AvanceId",
                table: "Mouvements",
                column: "AvanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mouvements_AvanceId",
                table: "Mouvements");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_AvanceId",
                table: "Mouvements",
                column: "AvanceId",
                unique: true,
                filter: "[AvanceId] IS NOT NULL");
        }
    }
}
