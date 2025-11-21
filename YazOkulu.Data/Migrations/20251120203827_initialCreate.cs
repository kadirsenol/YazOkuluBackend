using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazOkulu.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quota = table.Column<int>(type: "int", nullable: false),
                    faculty_id = table.Column<int>(type: "int", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modify_user_id = table.Column<int>(type: "int", nullable: true),
                    modify_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_user_id = table.Column<int>(type: "int", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "gsm_otps",
                columns: table => new
                {
                    gsm_otp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gsm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    otp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modify_user_id = table.Column<int>(type: "int", nullable: true),
                    modify_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_user_id = table.Column<int>(type: "int", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gsm_otps", x => x.gsm_otp_id);
                });

            migrationBuilder.CreateTable(
                name: "parameters",
                columns: table => new
                {
                    parameter_id = table.Column<int>(type: "int", nullable: false),
                    parent_parameter_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parameters", x => x.parameter_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gsm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    faculty_type_id = table.Column<int>(type: "int", nullable: true),
                    department_type_id = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role_type_id = table.Column<int>(type: "int", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modify_user_id = table.Column<int>(type: "int", nullable: true),
                    modify_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_user_id = table.Column<int>(type: "int", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_courses_is_deleted",
                table: "courses",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "i_x_gsm_otps_is_deleted",
                table: "gsm_otps",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "i_x_users_is_deleted",
                table: "users",
                column: "is_deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "gsm_otps");

            migrationBuilder.DropTable(
                name: "parameters");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
