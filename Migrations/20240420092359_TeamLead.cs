using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTasker.Migrations
{
    /// <inheritdoc />
    public partial class TeamLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamLeadId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamLeadId",
                table: "Projects");
        }
    }
}
