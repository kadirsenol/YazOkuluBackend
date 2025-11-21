using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazOkulu.Data.Migrations
{
    /// <inheritdoc />
    public partial class currentQuatoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "available_quota",
                table: "courses",
                newName: "current_quota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "current_quota",
                table: "courses",
                newName: "available_quota");
        }
    }
}
