using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazOkulu.Data.Migrations
{
    /// <inheritdoc />
    public partial class courseDepartmanAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "department_id",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department_id",
                table: "courses");
        }
    }
}
