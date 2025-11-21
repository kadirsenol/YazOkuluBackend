using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazOkulu.Data.Migrations
{
    /// <inheritdoc />
    public partial class applicationsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_parameters",
                table: "parameters");

            migrationBuilder.DropPrimaryKey(
                name: "pk_courses",
                table: "courses");

            migrationBuilder.AddPrimaryKey(
                name: "p_k__users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k__parameters",
                table: "parameters",
                column: "parameter_id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k__courses",
                table: "courses",
                column: "course_id");

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("pk_applications", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_applications_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applications_parameters_status_id",
                        column: x => x.status_id,
                        principalTable: "parameters",
                        principalColumn: "parameter_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_applications_course_id",
                table: "applications",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "i_x_applications_is_deleted",
                table: "applications",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "i_x_applications_status_id",
                table: "applications",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "i_x_applications_user_id",
                table: "applications",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropPrimaryKey(
                name: "p_k__users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "p_k__parameters",
                table: "parameters");

            migrationBuilder.DropPrimaryKey(
                name: "p_k__courses",
                table: "courses");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_parameters",
                table: "parameters",
                column: "parameter_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_courses",
                table: "courses",
                column: "course_id");
        }
    }
}
