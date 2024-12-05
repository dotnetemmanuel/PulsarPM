using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulsarPM.Migrations
{
    /// <inheritdoc />
    public partial class noboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_KanbanBoards_KanbanBoardId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanBoards_Projects_ProjectId",
                table: "KanbanBoards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KanbanBoards",
                table: "KanbanBoards");

            migrationBuilder.RenameTable(
                name: "KanbanBoards",
                newName: "KanbanBoard");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanBoards_ProjectId",
                table: "KanbanBoard",
                newName: "IX_KanbanBoard_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "KanbanBoardId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KanbanBoard",
                table: "KanbanBoard",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ProjectId",
                table: "Cards",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_KanbanBoard_KanbanBoardId",
                table: "Cards",
                column: "KanbanBoardId",
                principalTable: "KanbanBoard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanBoard_Projects_ProjectId",
                table: "KanbanBoard",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_KanbanBoard_KanbanBoardId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanBoard_Projects_ProjectId",
                table: "KanbanBoard");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ProjectId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KanbanBoard",
                table: "KanbanBoard");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "KanbanBoard",
                newName: "KanbanBoards");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanBoard_ProjectId",
                table: "KanbanBoards",
                newName: "IX_KanbanBoards_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "KanbanBoardId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KanbanBoards",
                table: "KanbanBoards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_KanbanBoards_KanbanBoardId",
                table: "Cards",
                column: "KanbanBoardId",
                principalTable: "KanbanBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanBoards_Projects_ProjectId",
                table: "KanbanBoards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
