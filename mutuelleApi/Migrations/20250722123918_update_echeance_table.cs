using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class update_echeance_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstPaye",
                table: "Echeances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstPaye",
                table: "Echeances");
        }
    }
}
