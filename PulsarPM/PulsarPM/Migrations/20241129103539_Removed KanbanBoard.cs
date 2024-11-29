using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulsarPM.Migrations
{
    /// <inheritdoc />
    public partial class RemovedKanbanBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_KanbanBoard_KanbanBoardId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "KanbanBoard");

            migrationBuilder.DropIndex(
                name: "IX_Cards_KanbanBoardId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "KanbanBoardId",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KanbanBoardId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KanbanBoard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanBoard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanBoard_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_KanbanBoardId",
                table: "Cards",
                column: "KanbanBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanBoard_ProjectId",
                table: "KanbanBoard",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_KanbanBoard_KanbanBoardId",
                table: "Cards",
                column: "KanbanBoardId",
                principalTable: "KanbanBoard",
                principalColumn: "Id");
        }
    }
}
