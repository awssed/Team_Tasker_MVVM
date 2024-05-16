using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTasker.Migrations
{
    /// <inheritdoc />
    public partial class TaskCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LeadCheck",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UserCheck",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadCheck",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserCheck",
                table: "Tasks");
        }
    }
}
