using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class update_avance_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateEnregistrement",
                table: "Avances",
                newName: "DateDemande");

            migrationBuilder.AddColumn<string>(
                name: "DateDecaissement",
                table: "Avances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDecaissement",
                table: "Avances");

            migrationBuilder.RenameColumn(
                name: "DateDemande",
                table: "Avances",
                newName: "DateEnregistrement");
        }
    }
}
