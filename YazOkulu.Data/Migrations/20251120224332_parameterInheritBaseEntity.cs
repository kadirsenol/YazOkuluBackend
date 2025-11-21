using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazOkulu.Data.Migrations
{
    /// <inheritdoc />
    public partial class parameterInheritBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "parameters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_user_id",
                table: "parameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "delete_date",
                table: "parameters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "delete_user_id",
                table: "parameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "guid",
                table: "parameters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "parameters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "modify_date",
                table: "parameters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modify_user_id",
                table: "parameters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "i_x_parameters_is_deleted",
                table: "parameters",
                column: "is_deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_parameters_is_deleted",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "create_user_id",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "delete_date",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "delete_user_id",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "guid",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "modify_date",
                table: "parameters");

            migrationBuilder.DropColumn(
                name: "modify_user_id",
                table: "parameters");
        }
    }
}
