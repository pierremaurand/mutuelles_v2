#nullable disable

namespace mutuelleApi.Migrations
{
    /// <inheritdoc />
    public partial class initial_bd_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Echeances_Avances_AvanceId",
                table: "Echeances");

            migrationBuilder.DropForeignKey(
                name: "FK_Echeances_Credits_CreditId",
                table: "Echeances");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Echeances_Avances_AvanceId",
                table: "Echeances",
                column: "AvanceId",
                principalTable: "Avances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Echeances_Credits_CreditId",
                table: "Echeances",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Echeances_Avances_AvanceId",
                table: "Echeances");

            migrationBuilder.DropForeignKey(
                name: "FK_Echeances_Credits_CreditId",
                table: "Echeances");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Echeances_Avances_AvanceId",
                table: "Echeances",
                column: "AvanceId",
                principalTable: "Avances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Echeances_Credits_CreditId",
                table: "Echeances",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
