using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTasker.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresss_Projects_ProjectId",
                table: "Progresss");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss");

            migrationBuilder.DropIndex(
                name: "IX_Progresss_ProjectId",
                table: "Progresss");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Progresss");

            migrationBuilder.RenameColumn(
                name: "TeamLeadId",
                table: "Projects",
                newName: "TeamLead");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Progresss",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss");

            migrationBuilder.RenameColumn(
                name: "TeamLead",
                table: "Projects",
                newName: "TeamLeadId");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Progresss",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Progresss",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Progresss_ProjectId",
                table: "Progresss",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresss_Projects_ProjectId",
                table: "Progresss",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresss_Tasks_TaskId",
                table: "Progresss",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }
    }
}
