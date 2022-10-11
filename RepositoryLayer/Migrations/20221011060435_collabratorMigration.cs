using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class collabratorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabratorTable",
                columns: table => new
                {
                    collabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collabratorEmail = table.Column<string>(nullable: false),
                    noteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabratorTable", x => x.collabId);
                    table.ForeignKey(
                        name: "FK_CollabratorTable_NoteTable_noteID",
                        column: x => x.noteID,
                        principalTable: "NoteTable",
                        principalColumn: "noteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabratorTable_noteID",
                table: "CollabratorTable",
                column: "noteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabratorTable");
        }
    }
}
