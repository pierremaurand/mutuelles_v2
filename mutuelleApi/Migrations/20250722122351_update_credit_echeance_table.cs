using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class update_credit_echeance_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateEnregistrement",
                table: "Credits",
                newName: "DateDemande");

            migrationBuilder.AlterColumn<int>(
                name: "CreditId",
                table: "Echeances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AvanceId",
                table: "Echeances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MontantCommission",
                table: "Echeances",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "DateDecaissement",
                table: "Credits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "MontantCommission",
                table: "Credits",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontantCommission",
                table: "Echeances");

            migrationBuilder.DropColumn(
                name: "DateDecaissement",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "MontantCommission",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "DateDemande",
                table: "Credits",
                newName: "DateEnregistrement");

            migrationBuilder.AlterColumn<int>(
                name: "CreditId",
                table: "Echeances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvanceId",
                table: "Echeances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
