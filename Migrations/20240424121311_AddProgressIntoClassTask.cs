using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTasker.Migrations
{
    /// <inheritdoc />
    public partial class AddProgressIntoClassTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Progresss",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progresss_TaskId",
                table: "Progresss",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss");

            migrationBuilder.DropIndex(
                name: "IX_Progresss_TaskId",
                table: "Progresss");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Progresss");
        }
    }
}
