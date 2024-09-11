using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeRH.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Atualizarnometabelaferias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBFerias_Colaboradores_ColaboradorId",
                table: "TBFerias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBFerias",
                table: "TBFerias");

            migrationBuilder.RenameTable(
                name: "TBFerias",
                newName: "Ferias");

            migrationBuilder.RenameIndex(
                name: "IX_TBFerias_ColaboradorId",
                table: "Ferias",
                newName: "IX_Ferias_ColaboradorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ferias",
                table: "Ferias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Colaboradores_ColaboradorId",
                table: "Ferias",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ferias_Colaboradores_ColaboradorId",
                table: "Ferias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ferias",
                table: "Ferias");

            migrationBuilder.RenameTable(
                name: "Ferias",
                newName: "TBFerias");

            migrationBuilder.RenameIndex(
                name: "IX_Ferias_ColaboradorId",
                table: "TBFerias",
                newName: "IX_TBFerias_ColaboradorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBFerias",
                table: "TBFerias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBFerias_Colaboradores_ColaboradorId",
                table: "TBFerias",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
