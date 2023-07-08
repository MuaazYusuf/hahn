using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedByColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById",
                unique: true,
                filter: "[UpdatedById] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UpdatedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UpdatedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Users");
        }
    }
}
