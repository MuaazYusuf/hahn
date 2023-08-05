using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkerAndAgentAndServiceProviderAndServiceRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_AgentId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_CreatedById",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CreatedById",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ServiceRequests",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionTakenAt",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "ServiceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestedById",
                table: "ServiceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceProviderId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProvider_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worker_ServiceProvider_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProvider",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Worker_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_AssignedToId",
                table: "ServiceRequests",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_RequestedById",
                table: "ServiceRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceProviderId",
                table: "Service",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_UserId",
                table: "Agent",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_UserId",
                table: "ServiceProvider",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_ServiceProviderId",
                table: "Worker",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_UserId",
                table: "Worker",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agent_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceProvider_ServiceProviderId",
                table: "Service",
                column: "ServiceProviderId",
                principalTable: "ServiceProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Agent_RequestedById",
                table: "ServiceRequests",
                column: "RequestedById",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_AssignedToId",
                table: "ServiceRequests",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agent_AgentId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceProvider_ServiceProviderId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Agent_RequestedById",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_AssignedToId",
                table: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "ServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_AssignedToId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_RequestedById",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceProviderId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ActionTakenAt",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ServiceRequests",
                newName: "CreatedById");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CreatedById",
                table: "ServiceRequests",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_CreatedById",
                table: "ServiceRequests",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
